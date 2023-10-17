using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisVaccination.Assign.Update
{
    class MedicineProcessor : BusinessBase
    {
        private List<HisMedicineBeanSplit> beanSpliters;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor(CommonParam paramUpdate)
            : base(paramUpdate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.beanSpliters = new List<HisMedicineBeanSplit>();
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal bool Run(List<VaccinationMetySDO> vaccinationMeties, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    deletes = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MEDICINE> olds = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);

                    //Danh sach exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> news = this.SplitBeanAndMakeData(olds, vaccinationMeties, expMest, ref newMedicineDic);

                    List<long> expMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MEDICINE_BEAN> oldBeans = new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds);

                    this.GetDiff(olds, news, newMedicineDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                    if (IsNotNullOrEmpty(inserts) && !this.hisExpMestMedicineCreate.CreateList(inserts))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (IsNotNullOrEmpty(updates) && !this.hisExpMestMedicineUpdate.UpdateList(updates, beforeUpdates))
                    {
                        throw new Exception("Rollback du lieu. Ket thuc nghiep vu");
                    }

                    if (sqls == null)
                    {
                        sqls = new List<string>();
                    }

                    List<long> deleteIds = IsNotNullOrEmpty(deletes) ? deletes.Select(o => o.ID).ToList() : null;

                    //Cap nhat thong tin bean
                    this.SqlUpdateBean(newMedicineDic, deleteIds, ref sqls);

                    //Xoa cac exp_mest_medicine ko dung.
                    //Luu y: can thuc hien xoa exp_mest_medicine sau khi da cap nhat bean (tranh bi loi fk)
                    this.SqlDeleteExpMestMedicine(deleteIds, ref sqls);

                    this.PassResult(olds, inserts, updates, deletes, ref resultData);
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Tach bean theo ReqMedicineData va tao ra exp_mest_medicine tuong ung
        /// </summary>
        /// <param name="toSplits">Danh sach ReqMedicineData dam bao ko co medicine_type_id nao trung nhau va cung thuoc 1 medi_stock_id</param>
        /// <param name="expMest"></param>
        /// <returns></returns>
        private List<HIS_EXP_MEST_MEDICINE> SplitBeanAndMakeData(List<HIS_EXP_MEST_MEDICINE> olds, List<VaccinationMetySDO> metyReqs, HIS_EXP_MEST expMest, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            List<HIS_MEDICINE_BEAN> medicineBeans = null;
            List<ExpMedicineTypeSDO> reqSplits = new List<ExpMedicineTypeSDO>();
            foreach (VaccinationMetySDO sdo in metyReqs)
            {
                List<long> expMedicineIds = olds != null ? olds
                    .Where(o => o.TDL_MEDICINE_TYPE_ID == sdo.MedicineTypeId
                        && o.PATIENT_TYPE_ID == sdo.PatientTypeId)
                    .Select(o => o.ID).ToList() : null;

                ExpMedicineTypeSDO tmp = new ExpMedicineTypeSDO();
                tmp.Amount = sdo.Amount;
                tmp.MedicineTypeId = sdo.MedicineTypeId;
                tmp.PatientTypeId = sdo.PatientTypeId;
                tmp.ExpMestMedicineIds = expMedicineIds;
                reqSplits.Add(tmp);
            }

            HisMedicineBeanSplit spliter = new HisMedicineBeanSplit(param);
            this.beanSpliters.Add(spliter);

            List<HIS_MEDICINE_PATY> medicinePaties = null;

            long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? Inventec.Common.DateTime.Get.StartDay() : null;

            if (!spliter.SplitByMedicineType(reqSplits, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
            {
                throw new Exception("Tach bean that bai. Rollback du lieu");
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();

            //Duyet theo tung yeu cau de tao ra du lieu duyet (exp_mest_medicine) tuong ung
            foreach (VaccinationMetySDO req in metyReqs)
            {
                //Do danh sach mety_req dam bao ko co medicine_type_id nao trung nhau ==> dung medicine_type_id de lay ra cac bean tuong ung
                List<HIS_MEDICINE_BEAN> reqBeans = medicineBeans.Where(o => o.TDL_MEDICINE_TYPE_ID == req.MedicineTypeId).ToList();

                List<HIS_EXP_MEST_MEDICINE> medicines = new List<HIS_EXP_MEST_MEDICINE>();
                if (!IsNotNullOrEmpty(reqBeans))
                {
                    throw new Exception("Ko tach duoc bean tuong ung voi MedicineTypeId:" + req.MedicineTypeId);
                }

                var group = reqBeans.GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });
                foreach (var tmp in group)
                {
                    //Luu y: vaccination, exp_mest duoc tao theo group medi_stock_id va patient_type_id
                    V_HIS_MEDI_STOCK mediStock = HisMediStockCFG.DATA.Where(o => o.ID == req.MediStockId).FirstOrDefault();
                    List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                    HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                    exp.EXP_MEST_ID = expMest.ID;
                    exp.TDL_VACCINATION_ID = expMest.VACCINATION_ID;
                    exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                    exp.TDL_MEDICINE_TYPE_ID = req.MedicineTypeId;
                    exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;
                    exp.VACCINATION_RESULT_ID = IMSys.DbConfig.HIS_RS.HIS_VACCINATION_RESULT.ID__UNINJECT;

                    //Neu ban bang gia nhap
                    if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                    {
                        exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                        exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                    }
                    else
                    {
                        HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == req.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                        if (paty == null)
                        {
                            throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + req.PatientTypeId);
                        }
                        exp.PRICE = paty.EXP_PRICE;
                        exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                    }

                    exp.TDL_MEDI_STOCK_ID = req.MediStockId;
                    exp.PATIENT_TYPE_ID = req.PatientTypeId;
                    exp.VACCINE_TURN = req.VaccineTurn;

                    medicines.Add(exp);
                    medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                }

                data.AddRange(medicines);
            }
            return data;
        }

        private void GetDiff(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> news, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> updates, ref List<HIS_EXP_MEST_MEDICINE> oldOfUpdates)
        {

            //Duyet du lieu truyen len de kiem tra thong tin thay doi
            if (!IsNotNullOrEmpty(news))
            {
                deletes = olds;
            }
            else if (!IsNotNullOrEmpty(olds))
            {
                inserts = news;
            }
            else
            {
                Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();

                //Duyet danh sach moi, nhung du lieu co trong moi ma ko co trong cu ==> can them moi
                foreach (HIS_EXP_MEST_MEDICINE newMedicine in news)
                {
                    HIS_EXP_MEST_MEDICINE old = olds
                        .Where(t => !IsDiff(newMedicine, t, newMedicineDic, oldBeans)
                            && t.AMOUNT == newMedicine.AMOUNT
                            && t.IS_EXPEND == newMedicine.IS_EXPEND
                            && t.IS_OUT_PARENT_FEE == newMedicine.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == newMedicine.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == newMedicine.PATIENT_TYPE_ID
                            && t.PRICE == newMedicine.PRICE
                            && t.SERE_SERV_PARENT_ID == newMedicine.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == newMedicine.VAT_RATIO
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMedicine);
                    }
                    else if (old.NUM_ORDER != newMedicine.NUM_ORDER || old.TUTORIAL != newMedicine.TUTORIAL || old.VACCINE_TURN != newMedicine.VACCINE_TURN)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.TUTORIAL = newMedicine.TUTORIAL;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.VACCINE_TURN = newMedicine.VACCINE_TURN;

                        oldOfUpdates.Add(oldOfUpdate);
                        updates.Add(old);
                    }
                }

                //Duyet danh sach cu, nhung du lieu co trong cu ma ko co trong moi ==> can xoa
                foreach (HIS_EXP_MEST_MEDICINE old in olds)
                {
                    HIS_EXP_MEST_MEDICINE newMedicine = news
                        .Where(t => !IsDiff(t, old, newMedicineDic, oldBeans)
                            && t.AMOUNT == old.AMOUNT
                            && t.IS_EXPEND == old.IS_EXPEND
                            && t.MEDICINE_ID == old.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == old.PATIENT_TYPE_ID
                            && t.PRICE == old.PRICE
                            && t.VAT_RATIO == old.VAT_RATIO
                        ).FirstOrDefault();

                    if (newMedicine == null)
                    {
                        deletes.Add(old);
                    }
                }
            }
        }

        private bool IsDiff(HIS_EXP_MEST_MEDICINE newMedicine, HIS_EXP_MEST_MEDICINE oldMedicine, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans)
        {
            List<long> oldBeanIdLst = IsNotNullOrEmpty(oldBeans) ? oldBeans.Where(o => o.EXP_MEST_MEDICINE_ID == oldMedicine.ID).Select(o => o.ID).ToList() : null;
            List<long> newBeanIdLst = newMedicineDic != null && newMedicineDic.ContainsKey(newMedicine) ? newMedicineDic[newMedicine] : null;
            return CommonUtil.IsDiff<long>(oldBeanIdLst, newBeanIdLst);
        }

        private void PassResult(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> inserts, List<HIS_EXP_MEST_MEDICINE> updates, List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> resultData)
        {
            if (IsNotNullOrEmpty(inserts) || IsNotNullOrEmpty(olds))
            {
                resultData = new List<HIS_EXP_MEST_MEDICINE>();
                if (IsNotNullOrEmpty(inserts))
                {
                    resultData.AddRange(inserts);
                }
                if (IsNotNullOrEmpty(updates))
                {
                    resultData.AddRange(updates);
                }
                if (IsNotNullOrEmpty(olds))
                {
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                    //clone, tranh thay doi du lieu tra ve qua bien ref
                    List<HIS_EXP_MEST_MEDICINE> remains = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(olds);
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, ref List<string> sqls)
        {
            //Can cap nhat cac bean ko dung truoc
            //Tranh truong hop bean duoc gan lai vao cac exp_mest_medicine tao moi
            if (IsNotNullOrEmpty(deleteExpMestMedicineIds))
            {
                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                sqls.Add(query2);
            }

            if (IsNotNullOrEmpty(newMedicineDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine
                foreach (HIS_EXP_MEST_MEDICINE key in newMedicineDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, key.ID);
                        sqls.Add(query);
                    }
                }
            }
        }

        private void SqlDeleteExpMestMedicine(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL, TDL_VACCINATION_ID = NULL, VACCINATION_RESULT_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.beanSpliters))
            {
                foreach (HisMedicineBeanSplit spliter in this.beanSpliters)
                {
                    spliter.RollBack();
                }
            }

            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
