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

namespace MOS.MANAGER.HisExpMest.Sale.Create
{
    class MedicineProcessor : BusinessBase
    {
        private HisExpMestMedicineCreate hisExpMestMedicineCreate;

        private List<long> recentMedicineBeanIds = new List<long>();

        internal MedicineProcessor(CommonParam paramCreate)
            : base(paramCreate)
        {
            this.hisExpMestMedicineCreate = new HisExpMestMedicineCreate(param);
        }

        /// <summary>
        /// Kiem tra cac bean ma client gui len co hop le khong. Bean can dam bao:
        /// + Dang khong bi khoa hoac Neu dang bi khoa thi session-key = session-key cua client truyen len
        /// + Tong so luong phai dung voi so luong client gui len
        /// </summary>
        /// <returns></returns>
        internal bool Run(string clientSessionKey, long? patientTypeId, List<long> medicineBeanIds, List<ExpMedicineTypeSDO> medicines, HIS_EXP_MEST expMest, ref List<HIS_EXP_MEST_MEDICINE> resultData, ref List<string> sqls, AutoEnum en = AutoEnum.NONE, long? axTime = null, string loginname = null, string username = null)
        {
            try
            {
                if (IsNotNullOrEmpty(medicines) && expMest != null)
                {
                    string sessionKey = SessionUtil.SessionKey(clientSessionKey);

                    List<HIS_MEDICINE_BEAN> medicineBeans = IsNotNullOrEmpty(medicineBeanIds) ? new HisMedicineBeanGet().GetByIds(medicineBeanIds) : null;
                    if (!IsNotNullOrEmpty(medicineBeans))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        LogSystem.Warn("medicine_bean_id ko hop le");
                        return false;
                    }

                    List<HIS_MEDICINE_BEAN> unavailables = medicineBeans
                        .Where(o => o.IS_ACTIVE == MOS.UTILITY.Constant.IS_TRUE && o.SESSION_KEY != sessionKey)
                        .ToList();

                    if (IsNotNullOrEmpty(unavailables))
                    {
                        LogSystem.Warn("Ton tai meidicine_bean khong cho phep lay: co session_key khac session_key client va dang bi khoa" + LogUtil.TraceData("unavailables", unavailables));
                        return false;
                    }

                    //List<long> medicineIds = medicineBeans.Select(o => o.MEDICINE_ID).ToList();
                    List<HIS_MEDICINE_PATY> medicinePaties = null;
                    if (patientTypeId.HasValue && medicineBeans.Any(a => !a.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || a.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE))
                    {
                        medicinePaties = new HisMedicinePatyGet().GetAppliedMedicinePaty(medicineBeans.Where(o => !o.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue || o.TDL_IS_SALE_EQUAL_IMP_PRICE.Value != Constant.IS_TRUE).Select(s => s.MEDICINE_ID).ToList(), patientTypeId.Value);
                    }

                    List<HIS_EXP_MEST_MEDICINE> expMestMedicines = new List<HIS_EXP_MEST_MEDICINE>();
                    Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> medicineDic = new Dictionary<HIS_EXP_MEST_MEDICINE, List<long>>();


                    //List<HIS_MEDICINE> hisMedicines = new HisMedicineGet().GetByIds(medicineIds);

                    var groupSdos = medicines.GroupBy(g => g.MedicineTypeId).ToList();

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
                            LogSystem.Warn("Ton tai meidicine_bean khong hop le: co so luong y/c khac voi tong so luong cua bean. So luong bean: " + beanAmount + LogUtil.TraceData("sdo", group.ToList()));
                            return false;
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

                            //neu co thong tin gia do nguoi dung nhap
                            if (firstSdo.Price.HasValue)
                            {
                                price = firstSdo.Price.Value;
                                vatRatio = firstSdo.VatRatio.HasValue ? firstSdo.VatRatio.Value : 0;
                                discount = !firstSdo.DiscountRatio.HasValue ?
                            0 : firstSdo.DiscountRatio.Value * amount * price * (1 + vatRatio);
                                isUseClientPrice = true;
                            }
                            //Neu khong thi lay theo chinh sach gia ban
                            else
                            {
                                if (first.TDL_IS_SALE_EQUAL_IMP_PRICE.HasValue && first.TDL_IS_SALE_EQUAL_IMP_PRICE.Value == UTILITY.Constant.IS_TRUE)
                                {
                                    price = first.TDL_MEDICINE_IMP_PRICE;
                                    vatRatio = first.TDL_MEDICINE_IMP_VAT_RATIO;
                                }
                                else
                                {
                                    HIS_MEDICINE_PATY mp = IsNotNullOrEmpty(medicinePaties) && patientTypeId.HasValue ?
                                    medicinePaties.Where(o => o.MEDICINE_ID == medicine.MEDICINE_ID && o.PATIENT_TYPE_ID == patientTypeId.Value).FirstOrDefault() : null;
                                    if (mp == null)
                                    {
                                        V_HIS_MEDICINE m = new HisMedicineGet().GetViewById(medicine.MEDICINE_ID);
                                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisMedicinePaty_KhongTonTaiTuongUngVoiLoaiThuoc, m.MEDICINE_TYPE_NAME);
                                        throw new Exception("Khong co thong tin gia ban tuong ung voi MEDICINE_ID:" + medicine.MEDICINE_ID + "; patientTypeId:" + patientTypeId);
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

                    if (!this.hisExpMestMedicineCreate.CreateList(expMestMedicines))
                    {
                        throw new Exception("Tao exp_mest_medicine that bai. Rollback du lieu");
                    }

                    this.SqlUpdateBean(expMest, sessionKey, medicineDic, en, ref sqls);

                    resultData = expMestMedicines;
                }
            }
            catch (Exception ex)
            {
                this.Rollback();
                LogSystem.Error(ex);
                return false;
            }
            return true;
        }

        private void SqlUpdateBean(HIS_EXP_MEST expMest, string sessionKey, Dictionary<HIS_EXP_MEST_MEDICINE, List<long>> useBeandIdDic, AutoEnum en, ref List<string> sqls)
        {
            if (IsNotNullOrEmpty(useBeandIdDic))
            {
                if (sqls == null)
                {
                    sqls = new List<string>();
                }

                List<long> medicineBeanIds = new List<long>();
                foreach (HIS_EXP_MEST_MEDICINE expMestMedicine in useBeandIdDic.Keys)
                {
                    List<long> beanIds = useBeandIdDic[expMestMedicine];
                    //cap nhat danh sach cac bean da dung
                    if (en == AutoEnum.APPROVE_EXPORT && expMest.EXP_MEST_STT_ID == IMSys.DbConfig.HIS_RS.HIS_EXP_MEST_STT.ID__DONE)
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL, MEDI_STOCK_ID = NULL WHERE %IN_CLAUSE% ", "ID");
                        sqls.Add(query);

                    }
                    else
                    {
                        string query = DAOWorker.SqlDAO.AddInClause(beanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 0, EXP_MEST_MEDICINE_ID = {0} WHERE %IN_CLAUSE% ", "ID");
                        query = string.Format(query, expMestMedicine.ID);
                        sqls.Add(query);

                    }
                    medicineBeanIds.AddRange(beanIds);
                }

                //cap nhat danh sach cac bean ko dung
                string query2 = DAOWorker.SqlDAO.AddNotInClause(medicineBeanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, EXP_MEST_MEDICINE_ID = NULL, IS_ACTIVE = 1 WHERE SESSION_KEY = '{0}' AND %IN_CLAUSE% ", "ID");
                query2 = string.Format(query2, sessionKey);
                sqls.Add(query2);

                this.recentMedicineBeanIds.AddRange(medicineBeanIds);
            }
        }

        internal void Rollback()
        {
            if (IsNotNullOrEmpty(this.recentMedicineBeanIds))
            {
                try
                {
                    string sql = DAOWorker.SqlDAO.AddInClause(this.recentMedicineBeanIds, "UPDATE HIS_MEDICINE_BEAN SET SESSION_KEY = NULL, SESSION_TIME = NULL, IS_ACTIVE = 1, EXP_MEST_MEDICINE_ID = NULL WHERE IS_ACTIVE = 0 AND EXP_MEST_MEDICINE_ID IS NOT NULL AND %IN_CLAUSE% ", "ID");
                    if (!DAOWorker.SqlDAO.Execute(sql))
                    {
                        LogSystem.Warn("Rollback du lieu his_medicine_bean that bai");
                    }
                }
                catch (Exception ex)
                {
                    LogSystem.Error(ex);
                    LogSystem.Warn("Rollback du lieu his_medicine_bean that bai");
                }
            }
            this.hisExpMestMedicineCreate.RollbackData();
        }
    }
}
