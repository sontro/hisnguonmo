using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
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

namespace MOS.MANAGER.HisServiceReq.Prescription.OutPatient.Update.SaleExpMest
{
    class MedicineProcessor : BusinessBase
    {
        private HisMedicineBeanSplit hisMedicineBeanSplit;
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisMedicineBeanSplit = new HisMedicineBeanSplit(param);
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        internal bool Run(List<PresOutStockMetySDO> serviceReqMeties, HIS_EXP_MEST expMest, long patientTypeId, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MEDICINE> inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> deletes = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MEDICINE> olds = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);

                    //Danh sach exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(olds, serviceReqMeties, expMest, patientTypeId, ref newMedicineDic);

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
                    List<HIS_EXP_MEST_MEDICINE> remains = olds;
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
                }
            }
        }

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, ref List<string> sqls)
        {
            //Cap nhat cac medicine gan voi cac exp_mest_medicine bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMedicineIds))
            {
                string query2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                sqls.Add(query2);
            }

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat medicine_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_medicine bi xoa nhung sau do lai duoc gan vao 1 exp_mest_medicine khac duoc tao moi
            if (IsNotNullOrEmpty(newMedicineDic))
            {
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
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
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
                            && t.IS_USE_CLIENT_PRICE == newMedicine.IS_USE_CLIENT_PRICE
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMedicine);
                    }
                    else if (old.TUTORIAL != newMedicine.TUTORIAL || old.NUM_ORDER != newMedicine.NUM_ORDER || old.USE_TIME_TO != newMedicine.USE_TIME_TO || old.DESCRIPTION != newMedicine.DESCRIPTION || old.PRES_AMOUNT != newMedicine.PRES_AMOUNT)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.TUTORIAL = newMedicine.TUTORIAL;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.USE_TIME_TO = newMedicine.USE_TIME_TO;
                        old.DESCRIPTION = newMedicine.DESCRIPTION;
                        old.PRES_AMOUNT = newMedicine.PRES_AMOUNT;

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
                            && t.IS_OUT_PARENT_FEE == old.IS_OUT_PARENT_FEE
                            && t.MEDICINE_ID == old.MEDICINE_ID
                            && t.PATIENT_TYPE_ID == old.PATIENT_TYPE_ID
                            && t.PRICE == old.PRICE
                            && t.SERE_SERV_PARENT_ID == old.SERE_SERV_PARENT_ID
                            && t.VAT_RATIO == old.VAT_RATIO
                            && t.IS_USE_CLIENT_PRICE == old.IS_USE_CLIENT_PRICE
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

        private List<HIS_EXP_MEST_MEDICINE> MakeData(List<HIS_EXP_MEST_MEDICINE> olds, List<PresOutStockMetySDO> serviceReqMeties, HIS_EXP_MEST expMest, long patientTypeId, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            List<ExpMedicineTypeSDO> medicines = new List<ExpMedicineTypeSDO>();
            //set exp_mest_medicine_id
            if (IsNotNullOrEmpty(serviceReqMeties))
            {
                foreach (PresOutStockMetySDO t in serviceReqMeties)
                {
                    if (t.MedicineTypeId.HasValue)
                    {
                        ExpMedicineTypeSDO sdo = new ExpMedicineTypeSDO();
                        sdo.Amount = t.Amount;
                        sdo.NumOrder = t.NumOrder;
                        sdo.PatientTypeId = patientTypeId;
                        sdo.MedicineTypeId = t.MedicineTypeId.Value;
                        sdo.ExpMestMedicineIds = olds != null ? olds.Where(o => o.TDL_MEDICINE_TYPE_ID == sdo.MedicineTypeId).Select(o => o.ID).ToList() : null;
                        sdo.UseTimeTo = t.UseTimeTo;
                        sdo.PresAmount = t.PresAmount;
                        medicines.Add(sdo);
                    }
                }
            }

            List<HIS_EXP_MEST_MEDICINE> data = new List<HIS_EXP_MEST_MEDICINE>();
            if (IsNotNullOrEmpty(medicines) && expMest != null)
            {
                List<HIS_MEDICINE_BEAN> medicineBeans = null;
                List<HIS_MEDICINE_PATY> medicinePaties = null;

                long? expiredDate = HisMediStockCFG.DONT_PRES_EXPIRED_ITEM ? expMest.TDL_INTRUCTION_TIME : null; //lay TDL_INTRUCTION_TIME chu ko lay TDL_INTRUCTION_DATE, vi truong nay do trigger trong DB xu ly --> tai thoi diem nay, chua co gia tri

                if (!this.hisMedicineBeanSplit.SplitByMedicineType(medicines, expMest.MEDI_STOCK_ID, expiredDate, null, null, ref medicineBeans, ref medicinePaties))
                {
                    throw new Exception("Ket thuc nghiep vu");
                }
                if (medicineDic == null)
                {
                    medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();
                }

                foreach (ExpMedicineTypeSDO sdo in medicines)
                {
                    var group = medicineBeans
                            .Where(o => o.TDL_MEDICINE_TYPE_ID == sdo.MedicineTypeId)
                            .GroupBy(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_IMP_PRICE, o.TDL_MEDICINE_IMP_VAT_RATIO });

                    foreach (var tmp in group)
                    {
                        List<HIS_MEDICINE_BEAN> beans = tmp.ToList();
                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = beans.Sum(o => o.AMOUNT);
                        exp.TDL_MEDICINE_TYPE_ID = sdo.MedicineTypeId;
                        exp.MEDICINE_ID = tmp.Key.MEDICINE_ID;

                        //Neu ban bang gia nhap
                        if (beans[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = tmp.Key.TDL_MEDICINE_IMP_PRICE;
                            exp.VAT_RATIO = tmp.Key.TDL_MEDICINE_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ?
                                medicinePaties.Where(o => o.PATIENT_TYPE_ID == sdo.PatientTypeId && o.MEDICINE_ID == tmp.Key.MEDICINE_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + tmp.Key.MEDICINE_ID + "va patient_type_id: " + sdo.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }

                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.NUM_ORDER = sdo.NumOrder;
                        exp.PATIENT_TYPE_ID = sdo.PatientTypeId;
                        exp.USE_TIME_TO = sdo.UseTimeTo;

                        exp.TUTORIAL = sdo.Tutorial;
                        exp.MORNING = sdo.Morning;
                        exp.NOON = sdo.Noon;
                        exp.AFTERNOON = sdo.Afternoon;
                        exp.EVENING = sdo.Evening;
                        exp.HTU_ID = sdo.HtuId;
                        exp.PRES_AMOUNT = sdo.PresAmount;
                        data.Add(exp);
                        medicineDic.Add(exp, beans.Select(o => o.ID).ToList());
                    }
                }
            }
            return data;
        }

        internal void Rollback()
        {
            this.hisMedicineBeanSplit.RollBack();
            this.hisExpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
