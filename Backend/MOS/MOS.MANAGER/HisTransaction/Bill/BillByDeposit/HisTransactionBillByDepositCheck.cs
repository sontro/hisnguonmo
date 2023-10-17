using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSereServBill;
using MOS.MANAGER.HisSereServDeposit;
using MOS.MANAGER.HisSeseDepoRepay;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillByDeposit
{
    class HisTransactionBillByDepositCheck : BusinessBase
    {
        internal HisTransactionBillByDepositCheck()
            : base()
        {

        }

        internal HisTransactionBillByDepositCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool ValidData(HisTransactionBillByDepositSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (!IsGreaterThanZero(data.TreatmentId)) throw new ArgumentNullException("data.TreatmentId");
                if (!IsGreaterThanZero(data.AccountBookId)) throw new ArgumentNullException("data.AccountBookId");
                if (!IsGreaterThanZero(data.PayformId)) throw new ArgumentNullException("data.PayformId");
                if (!IsGreaterThanZero(data.TransactionTime)) throw new ArgumentNullException("data.TransactionTime");
                if (!IsGreaterThanZero(data.WorkingRoomId)) throw new ArgumentNullException("data.WorkingRoomId");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                Inventec.Common.Logging.LogSystem.Warn(ex);
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

        internal bool VerifyTreatmentFee(long treatmentId, ref V_HIS_TREATMENT_FEE treatmentFee)
        {
            bool valid = true;
            try
            {
                treatmentFee = new HisTreatmentGet().GetFeeViewById(treatmentId);
                if (treatmentFee == null)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("TreatmentId invalid: " + treatmentId);
                }

                if (treatmentFee.IS_ACTIVE != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTreatment_HoSoDaDuyetKhoaTaiChinh);
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

        internal bool HasTransaction(long treatmentId, ref List<HIS_TRANSACTION> deposits, ref List<HIS_TRANSACTION> repays, ref List<HIS_TRANSACTION> bills)
        {
            bool valid = true;
            try
            {
                HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                filter.TREATMENT_ID = treatmentId;
                filter.IS_CANCEL = false;
                List<HIS_TRANSACTION> all = new HisTransactionGet().Get(filter);

                deposits = all != null ? all.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TU).ToList() : null;

                if (!IsNotNullOrEmpty(deposits))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_HoSoKhongCoGiaoDichTamUngDichVu);
                    return false;
                }

                repays = all != null ? all.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__HU).ToList() : null;
                bills = all != null ? all.Where(o => o.TRANSACTION_TYPE_ID == IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT && (o.SERE_SERV_AMOUNT ?? 0) > 0).ToList() : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool HasSereServ(long treatmentId, List<HIS_TRANSACTION> deposits, List<HIS_TRANSACTION> repays, List<HIS_TRANSACTION> bills, ref List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, ref List<HIS_SESE_DEPO_REPAY> seseDepoRepays, ref  List<HIS_SERE_SERV_BILL> sereServBills, ref  List<HIS_SERE_SERV> sereServs)
        {
            bool valid = true;
            try
            {
                sereServs = new HisSereServGet().GetByTreatmentId(treatmentId);
                if (!IsNotNullOrEmpty(sereServs))
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    throw new Exception("Khong lay duoc SereServ theo treatmentId: " + treatmentId);
                }

                if (IsNotNullOrEmpty(deposits))
                {
                    sereServDeposits = new HisSereServDepositGet().GetByDepositIds(deposits.Select(s => s.ID).ToList());
                }

                if (IsNotNullOrEmpty(repays))
                {
                    seseDepoRepays = new HisSeseDepoRepayGet().GetByRepayIds(repays.Select(s => s.ID).ToList());
                }

                if (IsNotNullOrEmpty(bills))
                {
                    sereServBills = new HisSereServBillGet().GetByBillIds(bills.Select(s => s.ID).ToList());
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

        internal bool NotHasDepositNormal(List<HIS_TRANSACTION> listDeposit)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> notDepositServices = listDeposit != null ? listDeposit.Where(o => !o.TDL_SERE_SERV_DEPOSIT_COUNT.HasValue || o.TDL_SERE_SERV_DEPOSIT_COUNT.Value <= 0).ToList() : null;
                if (IsNotNullOrEmpty(notDepositServices))
                {
                    string tranCodes = String.Join(",", notDepositServices.Select(s => s.TRANSACTION_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_TonTaiPhieuTamUngThuong, tranCodes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool NotHasRapayNormal(List<HIS_TRANSACTION> listRapay)
        {
            bool valid = true;
            try
            {
                List<HIS_TRANSACTION> notRepayServices = listRapay != null ? listRapay.Where(o => !o.TDL_SESE_DEPO_REPAY_COUNT.HasValue || o.TDL_SESE_DEPO_REPAY_COUNT.Value <= 0).ToList() : null;
                if (IsNotNullOrEmpty(notRepayServices))
                {
                    string tranCodes = String.Join(",", notRepayServices.Select(s => s.TRANSACTION_CODE).ToList());
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_TonTaiPhieuHoanUngThuong, tranCodes);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }

        internal bool HasDepositServiceNotBill(List<HIS_SERE_SERV_DEPOSIT> sereServDeposits, List<HIS_SESE_DEPO_REPAY> seseDepoRepays, List<HIS_SERE_SERV_BILL> sereServBills, List<HIS_SERE_SERV> sereServs, ref List<HIS_SERE_SERV_DEPOSIT> needBills)
        {
            bool valid = true;
            try
            {
                List<HIS_SERE_SERV_DEPOSIT> serviceDeposits = sereServDeposits;
                serviceDeposits = serviceDeposits != null ? serviceDeposits.Where(o => seseDepoRepays == null || !seseDepoRepays.Exists(e => e.SERE_SERV_DEPOSIT_ID == o.ID)).ToList() : null;
                serviceDeposits = serviceDeposits != null ? serviceDeposits.Where(o => sereServBills == null || !sereServBills.Exists(e => e.SERE_SERV_ID == o.SERE_SERV_ID)).ToList() : null;
                serviceDeposits = serviceDeposits != null ? serviceDeposits.Where(o => sereServs != null && sereServs.Exists(e => e.ID == o.SERE_SERV_ID && e.IS_NO_EXECUTE != Constant.IS_TRUE && (e.VIR_TOTAL_PATIENT_PRICE ?? 0) > 0)).ToList() : null;

                if (!IsNotNullOrEmpty(serviceDeposits))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_CacDichVuTamUngDaDuocThanhToan);
                    return false;
                }
                needBills = serviceDeposits;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsCashierRoom(WorkPlaceSDO workPlace)
        {
            try
            {
                if (workPlace == null || !workPlace.CashierRoomId.HasValue)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.BanKhongLamViecTaiPhongThuNgan);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }

        internal bool IsAllowAccountBook(long accountBookId, bool IsSplitByCashierDeposit, WorkPlaceSDO workPlace)
        {
            try
            {
                //chi kiem tra khi chon ghi nhan theo thu ngan
                if (IsSplitByCashierDeposit)
                {
                    List<HIS_CARO_ACCOUNT_BOOK> caroAccount = new HisCaroAccountBookGet().GetByAccountBookId(accountBookId);
                    if (!IsNotNullOrEmpty(caroAccount) || !caroAccount.Exists(o => o.CASHIER_ROOM_ID == workPlace.CashierRoomId))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoChuaDuocThietLapVaoPhong);
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return false;
        }
    }
}
