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
using MOS.SDO;
using MOS.UTILITY;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisExpMest.Sale.Update
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
        internal bool Run(HisExpMestSaleSDO sdo, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls, AutoEnum en = AutoEnum.NONE, long? axTime = null, string loginname = null, string username = null)
        {
            try
            {
                if (expMest != null)
                {
                    List<HIS_EXP_MEST_MEDICINE> inserts = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> deletes = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> updates = new List<HIS_EXP_MEST_MEDICINE>();
                    List<HIS_EXP_MEST_MEDICINE> beforeUpdates = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();

                    string sessionKey = SessionUtil.SessionKey(sdo.ClientSessionKey);

                    //Lay ra danh sach thong tin cu
                    List<HIS_EXP_MEST_MEDICINE> olds = new HisExpMestMedicineGet().GetByExpMestId(expMest.ID);
                    List<long> expMestMedicineIds = IsNotNullOrEmpty(olds) ? olds.Select(o => o.ID).ToList() : null;
                    List<HIS_MEDICINE_BEAN> oldBeans = new HisMedicineBeanGet().GetByExpMestMedicineIds(expMestMedicineIds);

                    //Danh sach exp_mest_medicine
                    List<HIS_EXP_MEST_MEDICINE> news = this.MakeData(sdo, expMest, olds, sessionKey, en, axTime, loginname, username, ref medicineDic);

                    this.GetDiff(olds, news, medicineDic, oldBeans, en, ref inserts, ref deletes, ref updates, ref beforeUpdates);

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

                    this.SqlUpdateBean(expMest, medicineDic, deleteIds, sessionKey, en, ref sqls);

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

        private void SqlDeleteExpMestMedicine(List<long> deleteIds, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(deleteIds))
            {
                string sql = DAOWorker.SqlDAO.AddInClause(deleteIds, "UPDATE HIS_EXP_MEST_MEDICINE SET IS_DELETE = 1, EXP_MEST_ID = NULL, MEDICINE_ID = NULL, TDL_MEDI_STOCK_ID = NULL, TDL_MEDICINE_TYPE_ID = NULL WHERE %IN_CLAUSE%", "ID");
                sqls.Add(sql);
            }
        }

        private void SqlUpdateBean(HIS_EXP_MEST expMest, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<long> deleteExpMestMedicineIds, string sessionKey, AutoEnum en, ref List<string> sqls)
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
                    if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        string sql = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(sql);
                    }
                    else
                    {
                        if (key.ID > 0) //chi xu ly voi cac exp_mest_medicine insert moi (duoc insert vao DB ==> co ID)
                        {
                            string sql = DAOWorker.SqlDAO.AddInClause(newMedicineDic[key], "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                            sql = string.Format(sql, key.ID);
                            sqls.Add(sql);
                        }
                    }
                    useBeanIds.AddRange(newMedicineDic[key]);
                }
            }

            //cap nhat danh sach cac bean ko dung nhung bi khoa trong qua trinh "take bean"
            string sql3 = DAOWorker.SqlDAO.AddNotInClause(useBeanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
            sql3 = string.Format(sql3, sessionKey);
            sqls.Add(sql3);
        }

        private void GetDiff(List<HIS_EXP_MEST_MEDICINE> olds, List<HIS_EXP_MEST_MEDICINE> news, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> newMedicineDic, List<HIS_MEDICINE_BEAN> oldBeans, AutoEnum en, ref List<HIS_EXP_MEST_MEDICINE> inserts, ref List<HIS_EXP_MEST_MEDICINE> deletes, ref List<HIS_EXP_MEST_MEDICINE> updates, ref List<HIS_EXP_MEST_MEDICINE> oldOfUpdates)
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
                    else if (en != AutoEnum.NONE || old.TUTORIAL != newMedicine.TUTORIAL || old.NUM_ORDER != newMedicine.NUM_ORDER || old.USE_TIME_TO != newMedicine.USE_TIME_TO || old.DESCRIPTION != newMedicine.DESCRIPTION)
                    {
                        HIS_EXP_MEST_MEDICINE oldOfUpdate = Mapper.Map<HIS_EXP_MEST_MEDICINE>(old);
                        old.TUTORIAL = newMedicine.TUTORIAL;
                        old.NUM_ORDER = newMedicine.NUM_ORDER;
                        old.USE_TIME_TO = newMedicine.USE_TIME_TO;

                        old.APPROVAL_TIME = newMedicine.APPROVAL_TIME;
                        old.APPROVAL_LOGINNAME = newMedicine.APPROVAL_LOGINNAME;
                        old.APPROVAL_USERNAME = newMedicine.APPROVAL_USERNAME;
                        old.IS_EXPORT = newMedicine.IS_EXPORT;
                        old.EXP_TIME = newMedicine.EXP_TIME;
                        old.EXP_LOGINNAME = newMedicine.EXP_LOGINNAME;
                        old.EXP_USERNAME = newMedicine.EXP_USERNAME;

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

        private List<HIS_EXP_MEST_MEDICINE> MakeData(HisExpMestSaleSDO sdo, HIS_EXP_MEST expMest, List<HIS_EXP_MEST_MEDICINE> olds, string sessionKey, AutoEnum en, long? axTime, string loginname, string username, ref Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic)
        {
            List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
            if (IsNotNullOrEmpty(sdo.Medicines) && expMest != null)
            {
                List<HIS_MEDICINE_BEAN> medicineBeans = IsNotNullOrEmpty(sdo.MedicineBeanIds) ? new HisMedicineBeanGet().GetByIds(sdo.MedicineBeanIds) : null;
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
                    throw new Exception("Ton tai medicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                }

                //List<long> medicineIds = medicineBeans.Select(o => o.MEDICINE_ID).ToList();
                List<HIS_MEDICINE_PATY> medicinePaties = null;

                //List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(medicineIds);

                if (sdo.PatientTypeId.HasValue && medicineBeans.Any(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE))
                {
                    medicinePaties = new HisMedicinePatyGet().GetAppliedMedicinePaty(medicineBeans.Where(o => !o.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || o.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MEDICINE_ID).ToList(), sdo.PatientTypeId.Value);
                }

                var groupSdos = sdo.Medicines.GroupBy(g => g.MedicineTypeId);

                //Duyet theo y/c cua client de tao ra exp_mest_medicine tuong ung
                foreach (var group in groupSdos)
                {
                    List<HIS_MEDICINE_BEAN> beans = medicineBeans
                        .Where(o => o.TDL_MEDICINE_TYPE_ID == group.Key && o.MEDI_STOCK_ID == expMest.MEDI_STOCK_ID)
                        .ToList();

                    ExpMedicineTypeSDO firstSdo = group.FirstOrDefault();

                    decimal beanAmount = IsNotNullOrEmpty(beans) ? beans.Sum(o => o.AMOUNT) : 0;
                    decimal sdoAmount = group.ToList().Sum(s => s.Amount);

                    //Neu so luong cua bean ko khop so luong do client y/c ==> reject
                    if (sdoAmount != beanAmount)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("Ton tai meidicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", sdo));
                    }

                    var groupMedicines = beans.Select(o => new { o.MEDICINE_ID, o.TDL_MEDICINE_TYPE_ID }).Distinct().ToList();
                    foreach (var medicine in groupMedicines)
                    {
                        List<HIS_MEDICINE_BEAN> usedBeans = beans.Where(o => o.MEDICINE_ID == medicine.MEDICINE_ID).ToList();
                        HIS_MEDICINE_BEAN first = usedBeans.FirstOrDefault();
                        decimal amount = usedBeans.Sum(o => o.AMOUNT);
                        decimal? discount = null;
                        decimal price = 0;
                        decimal vatRatio = 0;
                        bool isUseClientPrice = false;

                        //neu co thong tin doi tuong 
                        if (firstSdo.Price.HasValue)
                        {
                            price = firstSdo.Price.Value;
                            vatRatio = firstSdo.VatRatio.HasValue ? firstSdo.VatRatio.Value : 0;
                            discount = !firstSdo.DiscountRatio.HasValue ? 0 : firstSdo.DiscountRatio.Value * amount * price * (1 + vatRatio);
                            isUseClientPrice = true;
                        }
                        else
                        {
                            if (first.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && first.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == UTILITY.Constant.IS_TRUE)
                            {
                                price = first.TDL_MEDICINE_IMP_PRICE;
                                vatRatio = first.TDL_MEDICINE_IMP_VAT_RATIO;
                            }
                            else
                            {
                                HIS_MEDICINE_PATY mp = IsNotNullOrEmpty(medicinePaties) && sdo.PatientTypeId.HasValue ?
                                    medicinePaties.Where(o => o.MEDICINE_ID == medicine.MEDICINE_ID && o.PATIENT_TYPE_ID == sdo.PatientTypeId.Value).FirstOrDefault() : null;
                                if (mp == null)
                                {
                                    V_HIS_MEDICINE m = new HisMedicineGet().GetViewById(medicine.MEDICINE_ID);
                                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicinePaty_KhongTonTaiTuongUngVoiLoaiThuoc, m.MEDICINE_TYPE_NAME);
                                    throw new Exception("Khong co thong tin gia ban tuong ung voi MEDICINE_ID:" + medicine.MEDICINE_ID + "; patientTypeId:" + sdo.PatientTypeId);
                                }
                                price = mp.EXP_PRICE;
                                vatRatio = mp.EXP_VAT_RATIO;
                            }
                        }

                        HIS_EXP_MEST_MEDICINE exp = new HIS_EXP_MEST_MEDICINE();
                        exp.EXP_MEST_ID = expMest.ID;
                        exp.AMOUNT = amount;
                        exp.MEDICINE_ID = medicine.MEDICINE_ID;
                        exp.TDL_MEDICINE_TYPE_ID = medicine.TDL_MEDICINE_TYPE_ID;
                        exp.NUM_ORDER = firstSdo.NumOrder;
                        exp.PRICE = price;
                        exp.VAT_RATIO = vatRatio;
                        exp.DISCOUNT = discount;
                        exp.DESCRIPTION = firstSdo.Description;
                        exp.TUTORIAL = firstSdo.Tutorial;
                        exp.TDL_MEDI_STOCK_ID = expMest.MEDI_STOCK_ID;
                        exp.IS_USE_CLIENT_PRICE = isUseClientPrice ? (short?)Constant.IS_TRUE : null;
                        if (firstSdo.NumOfDays.HasValue && expMest.TDL_INTRUCTION_TIME.HasValue)
                        {
                            long dayCount = firstSdo.NumOfDays.Value == 0 ? 1 : firstSdo.NumOfDays.Value;

                            DateTime time = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(expMest.TDL_INTRUCTION_TIME.Value).Value;
                            DateTime useTimeTo = time.AddDays(dayCount - 1);
                            exp.USE_TIME_TO = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(useTimeTo);
                        }

                        if (en == AutoEnum.APPROVE)
                        {
                            exp.APPROVAL_TIME = axTime;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_USERNAME = username;
                        }
                        else if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                        {
                            exp.APPROVAL_TIME = axTime;
                            exp.APPROVAL_LOGINNAME = loginname;
                            exp.APPROVAL_USERNAME = username;

                            exp.IS_EXPORT = Constant.IS_TRUE;
                            exp.EXP_TIME = axTime;
                            exp.EXP_LOGINNAME = loginname;
                            exp.EXP_USERNAME = username;
                        }

                        expMestMedicines.Add(exp);
                        medicineDic.Add(exp, usedBeans.Select(o => o.ID).ToList());
                    }
                }
            }
            return expMestMedicines;
        }

        internal void Rollback()
        {
            this.hisExpMestMedicineCreate.RollbackData();
            this.hisExpMestMedicineUpdate.RollbackData();
        }
    }
}
