using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServDebt;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Debt.Create
{
    class HisTransactionDebtCheck : BusinessBase
    {
        internal HisTransactionDebtCheck()
            : base()
        {

        }

        internal HisTransactionDebtCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool VerifyRequireField(HisTransactionDebtSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsNotNull(data.Transaction)) throw new ArgumentNullException("data.Transaction");
                //if (!IsNotNullOrEmpty(data.SereServDebts)) throw new ArgumentNullException("data.SereServDebts");
                if (!IsGreaterThanZero(data.RequestRoomId)) throw new ArgumentNullException("data.RequestRoomId");
                if (!IsGreaterThanZero(data.Transaction.TREATMENT_ID ?? 0)) throw new ArgumentNullException("data.Transaction.TREATMENT_ID");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidAmount(HisTransactionDebtSDO data, bool checkTranAmount = true)
        {
            bool valid = true;
            try
            {
                if (data.SereServDebts.Any(a => a.DEBT_PRICE <= 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Ton tai sereServDebt co DEBT_PRICE <= 0");
                }
                //Tong so tien trong sere_serv_debt phai bang so tien trong transaction
                decimal sereServDebtTotal = data.SereServDebts.Sum(o => o.DEBT_PRICE);
                if (checkTranAmount && sereServDebtTotal != data.Transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Tong so tien trong sere_serv_debt ko khop voi so tien trong transaction. " + String.Format("TranAmount: {0}, DebtPrice: {1}", data.Transaction.AMOUNT, sereServDebtTotal));
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidSereServ(HisTransactionDebtSDO data, ref List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                //Lay danh sach sere_serv tuong ung voi ho so
                List<long> sereServIds = data.SereServDebts.Select(o => o.SERE_SERV_ID).Distinct().ToList();
                HisSereServFilterQuery filter = new HisSereServFilterQuery();
                filter.IDs = sereServIds;
                filter.TREATMENT_ID = data.Transaction.TREATMENT_ID.Value;
                List<HIS_SERE_SERV> ss = new HisSereServGet().Get(filter);

                List<long> invalidIds = sereServIds != null ? sereServIds.Where(o => ss == null || !ss.Exists(t => t.ID == o)).ToList() : null;

                if (IsNotNullOrEmpty(invalidIds))
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuKhongHopLe);
                    LogSystem.Warn("Loi du lieu. Ton tai sere_serv_id gui len ko co tren he thong hoac thuoc ho so dieu tri khac");
                    return false;
                }
                if (sereServIds.Count != ss.Count)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Ton tai 2 SereServDebts co cung 1 sereServId");
                    return false;
                }

                if (ss.Any(s => (s.VIR_TOTAL_PATIENT_PRICE ?? 0) == 0))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Error("Loi du lieu. Ton tai sere_serv vir_total_patient_price <= 0");
                    return false;
                }
                sereServs = ss;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool IsValidSereServDebt(List<HIS_SERE_SERV> sereServs, List<HIS_SERE_SERV_DEBT> sereServDebts, ref List<HIS_SERE_SERV_DEBT> oldSereServDebts)
        {
            bool valid = true;
            try
            {
                if (IsNotNullOrEmpty(sereServDebts) && IsNotNullOrEmpty(sereServs))
                {
                    //Lay danh sach sere_serv tuong ung voi ho so
                    List<long> sereServIds = sereServDebts.Select(o => o.SERE_SERV_ID).Distinct().ToList();

                    //Lay danh sach thong tin thanh toan (va chua bi huy) tuong ung voi sere_serv
                    List<HIS_SERE_SERV_DEBT> existsDebts = new HisSereServDebtGet().GetNoCancelBySereServIds(sereServIds);
                    List<HIS_SERE_SERV_DEBT> allDebts = new List<HIS_SERE_SERV_DEBT>();
                    allDebts.AddRange(sereServDebts);
                    if (IsNotNullOrEmpty(existsDebts))
                    {
                        allDebts.AddRange(existsDebts);
                    }

                    List<string> serviceNames = new List<string>();
                    foreach (HIS_SERE_SERV s in sereServs)
                    {
                        decimal totalBill = allDebts.Where(o => o.SERE_SERV_ID == s.ID).Sum(o => o.DEBT_PRICE);

                        //Luu y: check lech tien voi "Constant.PRICE_DIFFERENCE", de tranh truong hop lam tron 
                        //(4 so sau phan thap phan) 
                        if (totalBill - s.VIR_TOTAL_PATIENT_PRICE.Value > Constant.PRICE_DIFFERENCE)
                        {
                            serviceNames.Add(s.TDL_SERVICE_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(serviceNames))
                    {
                        string nameStr = string.Join(",", serviceNames);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_VuotQuaSoTienNo, nameStr);
                        return false;
                    }
                    oldSereServDebts = existsDebts;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool VerifyDeptType(HisTransactionDebtSDO data, HIS_TREATMENT treat, ref bool isPause)
        {
            bool valid = true;
            try
            {
                isPause = (treat.IS_PAUSE.HasValue && treat.IS_PAUSE == Constant.IS_TRUE);
                if (isPause)
                {
                    V_HIS_TREATMENT_FEE_1 treatFee = new HisTreatmentGet().GetFeeView1ById(treat.ID);
                    HisTransactionFilterQuery tFilter = new HisTransactionFilterQuery();
                    tFilter.TREATMENT_ID = treat.ID;
                    tFilter.TRANSACTION_TYPE_ID = IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO;
                    tFilter.IS_CANCEL = false;
                    List<HIS_TRANSACTION> listDept = new HisTransactionGet().Get(tFilter);
                    List<HIS_TRANSACTION> deptTreats = listDept != null ? listDept.Where(o => o.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__TREAT).ToList() : null;
                    if (IsNotNullOrEmpty(deptTreats))
                    {
                        string codes = String.Join(",", deptTreats.Select(s => s.TRANSACTION_CODE).ToList());
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_HoSoDaDuocChoNo, codes);
                        return false;
                    }
                    List<HIS_TRANSACTION> deptServices = listDept != null ? listDept.Where(o => o.DEBT_TYPE == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION.DEBT_TYPE__SERVICE).ToList() : null;
                    decimal unPaid = (new HisTreatmentGet().GetUnpaid(treatFee) ?? 0);
                    decimal avaliable = (new HisTreatmentGet().GetAvailableAmount(treatFee) ?? 0);

                    if (unPaid <= Constant.PRICE_DIFFERENCE)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_HoSoDaChotHetNo);
                        throw new Exception("So tien chot no <= 0.0001: " + unPaid);
                    }

                    if (Math.Abs(unPaid - data.Transaction.AMOUNT) > Constant.PRICE_DIFFERENCE)
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                        throw new Exception("So tien chot no do server tinh la: " + unPaid + " khac voi so tien do client y/c la: " + data.Transaction.AMOUNT);
                    }
                    if (avaliable > 0)
                    {
                        data.Transaction.KC_AMOUNT = avaliable;
                    }
                }
                else
                {
                    if (!IsNotNullOrEmpty(data.SereServDebts))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                        throw new ArgumentNullException("data.SereServDebts");
                    }
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
