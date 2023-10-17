using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.MANAGER.HisMedicine;
using MOS.MANAGER.HisMedicineBean;
using MOS.MANAGER.HisMedicineBean.Handle;
using MOS.MANAGER.HisMedicinePaty;
using MOS.MANAGER.HisSereServ.Update;
using MOS.MANAGER.HisSereServ.Update.Package;
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription.Subclinical.Update
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;
        private HisExpMestMedicineUpdate hisExpMestMedicineUpdate;

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
            this.hisExpMestMedicineUpdate = new HisExpMestMedicineUpdate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(SubclinicalPresSDO sdo, HisSereServPackage37 processPackage37, HisSereServPackageBirth processPackageBirth, HisSereServPackagePttm processPackagePttm, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls)
        {
            try
            {
                if (expMest != null)
                {
                    if (inserts == null)
                    {
                        inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    }
                    if (deletes == null)
                    {
                        deletes = new List<HIS_EXP_MEST_MEDICINE>();
                    }

                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);

                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MEDICINE> olds = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                    List<long> expMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MEDICINE_BEAN> oldBeans = new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds);

                    //Danh sach exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(sdo, expMest, olds, sessionKey, ref medicineDic);

                    
                    if (IsNotNullOrEmpty(news))
                    {
                        //Xu ly de ap dung chinh sach gia 3 ngay 7 ngay
                        processPackage37.Apply3Day7Day(null, null, news, sdo.InstructionTime);
                        //Xu ly de ap dung goi de
                        processPackageBirth.Run(news, news[0].SERE_SERV_PARENT_ID);
                        //Xu ly de ap dung goi phau thuat tham my
                        processPackagePttm.Run(news, news[0].SERE_SERV_PARENT_ID, sdo.InstructionTime);
                    }


                    this.GetDiff(olds, news, medicineDic, oldBeans, ref inserts, ref deletes, ref updates, ref beforeUpdates);

                    List<long> medicineIds = news != null ? news.Where(o => o.MEDICINE_ID.HasValue).Select(o => o.MEDICINE_ID.Value).ToList() : null;
                    List<V_HIS_MEDICINE_2> choosenMedicines = new HisMedicineGet().GetView2ByIds(medicineIds);

                    if (!new HisServiceReqPresCheck(param).IsValidMedicineWithBidDate(choosenMedicines, news, new List<HIS_EXP_MEST>() { expMest }))
                    {
                        throw new Exception("IsValidMedicineWithBidDate false. Rollback du lieu");
                    }

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

                    this.SqlUpdateBean(medicineDic, deleteIds, sessionKey, ref sqls);

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
                    Mapper.CreateMap<HIS_EXP_MEST_MEDICINE, HIS_EXP_MEST_MEDICINE>();
                    //clone, tranh thay doi du lieu tra ve qua bien ref
                    List<HIS_EXP_MEST_MEDICINE> remains = Mapper.Map<List<HIS_EXP_MEST_MEDICINE>>(olds);
                    remains.RemoveAll(o => deletes != null && deletes.Exists(t => t.ID == o.ID));
                    remains.RemoveAll(o => updates != null && updates.Exists(t => t.ID == o.ID));
                    resultData.AddRange(remains);
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

        private void SqlUpdateBean(Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, string sessionKey, ref List<string> sqls)
        {
            //Cap nhat danh sach cac bean gan voi cac exp_mest_medicine bi xoa
            if (IsNotNullOrEmpty(deleteExpMestMedicineIds))
            {
                string sql2 = DAOWorker.SqlDAO.AddInClause(deleteExpMestMedicineIds, "UPDATE HIS_MEDICINE_BEAN SET IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE %IN_CLAUSE% ", "EXP_MEST_MEDICINE_ID");
                sqls.Add(sql2);
            }

            List<long> useBeanIds = new List<long>();

            //Cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine cua phieu xuat
            //Luu y: can thuc hien sau viec cap nhat medicine_bean o tren. Tranh truong hop, bean gan 
            //vao 1 exp_mest_medicine bi xoa nhung sau do lai duoc gan vao 1 exp_mest_medicine khac duoc tao moi
            if (IsNotNullOrEmpty(newMedicineDic))
            {
                //cap nhat danh sach cac bean da dung tuong ung voi cac exp_mest_medicine
                foreach (HIS_EXP_MEST_MEDICINE key in newMedicineDic.Keys)
                {
                    if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        sql = string.Format(sql, key.ID);
                        sqls.Add(sql);
                    }
                    useBeanIds.AddRange(newMedicineDic[key]);
                }
            }

            //cap nhat danh sach cac bean ko dung nhung bi khoa trong qua trinh "take bean"
            string sql3 = DAOWorker.SqlDAO.AddNotInClause(useBeanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
            sql3 = string.Format(sql3, sessionKey);
            sqls.Add(sql3);
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
                            && t.EXPEND_TYPE_ID == newMedicine.EXPEND_TYPE_ID
                        ).FirstOrDefault();

                    if (old == null)
                    {
                        inserts.Add(newMedicine);
                    }
                    else if (old.TUTORIAL != newMedicine.TUTORIAL || old.NUM_ORDER != newMedicine.NUM_ORDER || old.USE_TIME_TO != newMedicine.USE_TIME_TO || old.PRES_AMOUNT != newMedicine.PRES_AMOUNT)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.TUTORIAL = newMedicine.TUTORIAL;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.USE_TIME_TO = newMedicine.USE_TIME_TO;
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
                            && t.EXPEND_TYPE_ID == old.EXPEND_TYPE_ID
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

        private List<HIS_EXP_MEST_MEDICINE> MakeData(SubclinicalPresSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> olds, string sessionKey, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            if (IsNotNullOrEmpty(sdo.Medicines) && expMest != null)
            {
                List<long> medicineBeanIds = new List<long>();
                foreach (PresMedicineSDO m in sdo.Medicines)
                {
                    medicineBeanIds.AddRange(m.MedicineBeanIds);
                }

                List<HIS_MEDICINE_BEAN> medicineBeans = IsNotNullOrEmpty(medicineBeanIds) ? new HisMedicineBeanGet().GetByIds(medicineBeanIds) : null;
                if (!IsNotNullOrEmpty(medicineBeans))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("medicine_bean_id ko hop le");
                }

                List<long> oldExpMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                List<HIS_MEDICINE_BEAN> unavailables = medicineBeans
                    .Where(o => o.IS_ACTIVE == IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__FALSE && o.SESSION_KEY != sessionKey && (oldExpMestMedicineIds == null || !o.EXP_MEST_MEDICINE_ID.HasValue || !oldExpMestMedicineIds.Contains(o.EXP_MEST_MEDICINE_ID.Value)))
                    .ToList();

                if (IsNotNullOrEmpty(unavailables))
                {
                    throw new Exception("Ton tai meidicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa nhung ko gan vao exp_mest_medicine cu~" + LogUtil.TraceData("unavailables", unavailables));
                }

                //Neu trong d/s bean co bean ko co cau hinh "ban bang gia nhap" thi lay chinh sach gia,
                //phuc vu lay chinh sach gia
                List<long> medicineIds = medicineBeans
                    .Where(o => o.TDL_IS_SALE_EQUAL_IMP_PRICE != MOS.UTILITY.Constant.IS_TRUE)
                    .Select(o => o.MEDICINE_ID).Distinct().ToList();

                List<HIS_MEDICINE_PATY> medicinePaties = null;
                if (IsNotNullOrEmpty(medicineIds))
                {
                    List<long> patientTypeIds = sdo.Medicines.Select(o => o.PatientTypeId).Distinct().ToList();

                    HisMedicinePatyFilterQuery filter = new HisMedicinePatyFilterQuery();
                    filter.MEDICINE_IDs = medicineIds;
                    filter.PATIENT_TYPE_IDs = patientTypeIds;
                    medicinePaties = new HisMedicinePatyGet().Get(filter);
                }

                List<HIS_EXP_MEST_MEDICINE> result = new List<HIS_EXP_MEST_MEDICINE>();
                //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                foreach (PresMedicineSDO s in sdo.Medicines)
                {
                    List<HIS_MEDICINE_BEAN> beans = medicineBeans
                        //neu co ko thong tin medicine_id thi lay theo medicine_id
                        .Where(o => ((o.TDL_MEDICINE_TYPE_ID == s.MedicineTypeId && !s.MedicineId.HasValue) || o.MEDICINE_ID == s.MedicineId)
                            && s.MedicineBeanIds != null && s.MedicineBeanIds.Contains(o.ID))
                        .OrderBy(o => o.MEDICINE_ID)
                        .ToList();

                    decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;

                    //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                    if (s.Amount != beanAmount)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai medicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                    }

                    //Group theo medicine_id de tao ra exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    var groupMedicines = beans.Select(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID }).Distinct().ToList();

                    foreach (var medicine in groupMedicines)
                    {
                        List<HIS_MEDICINE_BEAN> b = beans.Where(o => o.MEDICINE_ID == medicine.MEDICINE_ID).ToList();
                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.TDL_SERVICE_REQ_ID = expMest.SERVICE_REQ_ID;
                        exp.TDL_TREATMENT_ID = expMest.TDL_TREATMENT_ID;
                        exp.AMOUNT = b.Sum(o => o.AMOUNT);
                        exp.IS_EXPEND = s.IsExpend ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.PATIENT_TYPE_ID = s.PatientTypeId;
                        exp.SERE_SERV_PARENT_ID = s.SereServParentId;
                        exp.IS_OUT_PARENT_FEE = s.IsOutParentFee && s.SereServParentId.HasValue ? new Nullable<short>(MOS.UTILITY.Constant.IS_TRUE) : null;
                        exp.MEDICINE_ID = medicine.MEDICINE_ID;
                        exp.NUM_ORDER = s.NumOrder;
                        exp.SPEED = s.Speed;
                        exp.USE_ORIGINAL_UNIT_FOR_PRES = s.UseOriginalUnitForPres ? (short?)Constant.IS_TRUE : null;

                        //tam thoi chua co danh muc, vi hien tai chi co 1 loai, nen fix code gia tri 1 
                        exp.EXPEND_TYPE_ID = s.IsBedExpend ? new Nullable<long>(1) : null;

                        //Neu ban bang gia nhap
                        if (b[0].TDL_IS_SALE_EQUAL_IMP_PRICE == MOS.UTILITY.Constant.IS_TRUE)
                        {
                            exp.PRICE = b[0].TDL_MEDICINE_IMP_PRICE;
                            exp.VAT_RATIO = b[0].TDL_MEDICINE_IMP_VAT_RATIO;
                        }
                        else
                        {
                            HIS_MEDICINE_PATY paty = IsNotNullOrEmpty(medicinePaties) ? medicinePaties.Where(o => o.PATIENT_TYPE_ID == s.PatientTypeId && o.MEDICINE_ID == medicine.MEDICINE_ID).FirstOrDefault() : null;
                            if (paty == null)
                            {
                                throw new Exception("Khong ton tai chinh sach gia tuong ung voi medicine_id: " + medicine.MEDICINE_ID + "va patient_type_id: " + s.PatientTypeId);
                            }
                            exp.PRICE = paty.EXP_PRICE;
                            exp.VAT_RATIO = paty.EXP_VAT_RATIO;
                        }
                        exp.TUTORIAL = s.Tutorial;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.TDL_MEDICINE_TYPE_ID = medicine.TDL_MEDICINE_TYPE_ID;
                        exp.PREVIOUS_USING_COUNT = s.PreviousUsingCount;
                        exp.PRES_AMOUNT = s.PresAmount;
                        if (s.NumOfDays.HasValue)
                        {
                            long dayCount = s.NumOfDays.Value == 0 ? 1 : s.NumOfDays.Value;
                            
                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(sdo.UseTime > 0 ? sdo.UseTime.Value : sdo.InstructionTime).Value;
                            DateTime useTimeTo = time.AddDays(dayCount - 1);
                            exp.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }
                        expMestMedicines.Add(exp);
                        medicineDic.Add(exp, b.Select(o => o.ID).ToList());
                    }
                    result.AddRange(expMestMedicines);
                }
                return result;
            }
            return null;
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
