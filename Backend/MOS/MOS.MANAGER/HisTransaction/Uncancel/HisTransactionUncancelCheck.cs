using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTransactionExp;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Uncancel
{
    class HisTransactionUncancelCheck : BusinessBase
    {
        internal HisTransactionUncancelCheck()
            : base()
        {

        }

        internal HisTransactionUncancelCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsAllow(HisTransactionUncancelSDO sdo, V_HIS_CASHIER_ROOM cashierRoom, ref HIS_TRANSACTION transaction)
        {
            bool valid = true;
            try
            {
                HIS_TRANSACTION trans = new HisTransactionGet().GetById(sdo.TransactionId);

                if (trans == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("transactionId ko hop le");
                    return false;
                }

                if (cashierRoom.ID != trans.CANCEL_CASHIER_ROOM_ID)
                {
                    V_HIS_CASHIER_ROOM cancelCashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == trans.CANCEL_CASHIER_ROOM_ID.Value).FirstOrDefault();
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDoPhongKhacHuy, trans.TRANSACTION_CODE, cancelCashierRoom.CASHIER_ROOM_NAME);
                    return false;
                }

                if (trans.IS_CANCEL != MOS.UTILITY.Constant.IS_TRUE)
                {
                    valid = false;
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichChuaBiHuy, trans.TRANSACTION_CODE);
                }

                if (trans.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__NO
                    || trans.IS_DEBT_COLLECTION == Constant.IS_TRUE
                    || trans.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_VACCIN)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("TRANSACTION_TYPE_ID = NO hoac IS_DEBT_COLLECTION = 1 hoac SALE_TYPE_ID = VACCIN");
                }

                transaction = trans;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidBill(HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_BILL> sereServBills, ref List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_BILL> ssBills = null;

                if (transaction.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && !transaction.SALE_TYPE_ID.HasValue)
                {
                    ssBills = new HisSereServBillGet().GetByBillId(transaction.ID);
                }

                if (IsNotNullOrEmpty(ssBills))
                {
                    List<string> deleteds = ssBills.Where(o => !o.TDL_SERVICE_REQ_ID.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList();
                    if (IsNotNullOrEmpty(deleteds))
                    {
                        string s = string.Join(",", deleteds);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaBiHuy, s);
                        return false;
                    }
                    List<long> sereServIds = ssBills.Select(o => o.SERE_SERV_ID).ToList();
                    List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(sereServIds);

                    List<string> changePrices = new List<string>();
                    foreach (HIS_SERE_SERV_BILL sb in ssBills)
                    {
                        HIS_SERE_SERV sereServ = ss != null ? ss.Where(o => o.ID == sb.SERE_SERV_ID).FirstOrDefault() : null;
                        if (sereServ == null || sereServ.VIR_TOTAL_PATIENT_PRICE < sb.PRICE)
                        {
                            changePrices.Add(sereServ.TDL_SERVICE_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(changePrices))
                    {
                        string s = string.Join(",", changePrices);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaBiThayDoiGia, s);
                        return false;
                    }

                    List<HIS_SERE_SERV_BILL> all = new HisSereServBillGet().GetBySereServIds(sereServIds);
                    List<string> others = IsNotNullOrEmpty(all) ? all.Where(o => !ssBills.Exists(t => t.ID == o.ID) && !o.IS_CANCEL.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                    if (IsNotNullOrEmpty(others))
                    {
                        string s = string.Join(",", others);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaDuocThanhToan, s);
                        return false;
                    }

                    sereServBills = ssBills;
                    sereServs = ss;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidDeposit(HIS_TRANSACTION transaction, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, ref List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_DEPOSIT> ssDeposits = null;

                if (transaction.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU && !transaction.SALE_TYPE_ID.HasValue)
                {
                    ssDeposits = new HisSereServDepositGet().GetByDepositId(transaction.ID);
                }

                if (IsNotNullOrEmpty(ssDeposits))
                {
                    List<string> deleteds = ssDeposits.Where(o => !o.TDL_SERVICE_REQ_ID.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList();
                    if (IsNotNullOrEmpty(deleteds))
                    {
                        string s = string.Join(",", deleteds);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaBiHuy, s);
                        return false;
                    }
                    List<long> sereServIds = ssDeposits.Select(o => o.SERE_SERV_ID).ToList();
                    List<HIS_SERE_SERV> ss = new HisSereServGet().GetByIds(sereServIds);

                    List<string> changePrices = new List<string>();
                    foreach (HIS_SERE_SERV_DEPOSIT sb in ssDeposits)
                    {
                        HIS_SERE_SERV sereServ = ss != null ? ss.Where(o => o.ID == sb.SERE_SERV_ID).FirstOrDefault() : null;
                        if (sereServ == null || sereServ.VIR_TOTAL_PATIENT_PRICE < sb.AMOUNT)
                        {
                            changePrices.Add(sereServ.TDL_SERVICE_NAME);
                        }
                    }

                    if (IsNotNullOrEmpty(changePrices))
                    {
                        string s = string.Join(",", changePrices);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaBiThayDoiGia, s);
                        return false;
                    }

                    List<HIS_SERE_SERV_DEPOSIT> all = new HisSereServDepositGet().GetBySereServIds(sereServIds);
                    List<string> others = IsNotNullOrEmpty(all) ? all.Where(o => !ssDeposits.Exists(t => t.ID == o.ID) && !o.IS_CANCEL.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                    if (IsNotNullOrEmpty(others))
                    {
                        string s = string.Join(",", others);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaDuocTamUng, s);
                        return false;
                    }

                    sereServDeposits = ssDeposits;
                    sereServs = ss;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidRepay(HIS_TRANSACTION transaction, ref List<HIS_SESE_DEPO_REPAY> seseDepoRepays)
        {
            bool valid = true;
            try
            {
                List<HIS_SESE_DEPO_REPAY> ssDepoRepays = null;

                if (transaction.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU && !transaction.SALE_TYPE_ID.HasValue)
                {
                    ssDepoRepays = new HisSeseDepoRepayGet().GetByRepayId(transaction.ID);
                }

                if (IsNotNullOrEmpty(ssDepoRepays))
                {
                    List<long> ssDepositIds = ssDepoRepays.Select(o => o.SERE_SERV_DEPOSIT_ID).ToList();
                    List<HIS_SERE_SERV_DEPOSIT> ssDeposits = new HisSereServDepositGet().GetByIds(ssDepositIds);

                    List<HIS_SERE_SERV_DEPOSIT> canceleds = IsNotNullOrEmpty(ssDeposits) ? ssDeposits.Where(o => o.IS_CANCEL == Constant.IS_TRUE).ToList() : null;
                    if (IsNotNullOrEmpty(canceleds))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichTamUngDaBiHuy);
                        return false;
                    }

                    List<HIS_SESE_DEPO_REPAY> repays = new HisSeseDepoRepayGet().GetBySereServDepositIds(ssDepositIds);
                    List<string> others = IsNotNullOrEmpty(repays) ? repays.Where(o => !ssDepoRepays.Exists(t => t.ID == o.ID) && !o.IS_CANCEL.HasValue).Select(o => o.TDL_SERVICE_NAME).ToList() : null;
                    if (IsNotNullOrEmpty(others))
                    {
                        string s = string.Join(",", others);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_DichVuDaDuocHoanUng, s);
                        return false;
                    }
                    seseDepoRepays = ssDepoRepays;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValidExpMest(HIS_TRANSACTION transaction, ref List<HIS_EXP_MEST> expMests)
        {
            bool valid = true;
            try
            {
                if (transaction.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && transaction.SALE_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_SALE_TYPE.ID__SALE_EXP)
                {
                    List<HIS_TRANSACTION_EXP> transactionExps = new HisTransactionExpGet().GetByTransactionId(transaction.ID);
                    List<string> deleted = IsNotNullOrEmpty(transactionExps) ? transactionExps
                        .Where(o => o.TDL_EXP_MEST_CODE != null && !o.EXP_MEST_ID.HasValue)
                        .Select(o => o.TDL_EXP_MEST_CODE).ToList() : null;

                    if (IsNotNullOrEmpty(deleted))
                    {
                        string s = string.Join(",", deleted);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhieuXuatBanDaBiHuy, s);
                        return false;
                    }

                    List<long> expMestIds = IsNotNullOrEmpty(transactionExps) ? transactionExps.Select(o => o.EXP_MEST_ID.Value).ToList() : null;

                    List<HIS_EXP_MEST> exps = new HisExpMestGet().GetByIds(expMestIds);
                    decimal totalPrice = IsNotNullOrEmpty(exps) ? exps.Sum(o => o.TDL_TOTAL_PRICE.Value) : 0;

                    if (Math.Abs(transaction.AMOUNT - totalPrice) > Constant.PRICE_DIFFERENCE)
                    {
                        List<string> expMestCodes = exps.Select(o => o.EXP_MEST_CODE).ToList();
                        string s = string.Join(",", expMestCodes);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhieuXuatDaBiThayDoi, s);
                        return false;
                    }

                    List<string> otherBills = IsNotNullOrEmpty(exps) ? exps.Where(o => o.BILL_ID.HasValue && o.BILL_ID != transaction.ID).Select(o => o.EXP_MEST_CODE).ToList() : null;
                    if (IsNotNullOrEmpty(otherBills))
                    {
                        string s = string.Join(",", otherBills);
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_PhieuXuatDaDuocThanhToan, s);
                        return false;
                    }
                    expMests = exps;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }
    }
}
