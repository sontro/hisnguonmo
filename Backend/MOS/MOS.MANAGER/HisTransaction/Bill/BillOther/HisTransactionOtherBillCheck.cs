using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Bill.BillOther
{
    class HisTransactionOtherBillCheck : BusinessBase
    {
        internal HisTransactionOtherBillCheck()
            : base()
        {

        }

        internal HisTransactionOtherBillCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool IsValidAmount(HIS_TRANSACTION transaction)
        {
            if (transaction != null)
            {
                //so tien mien giam
                decimal exemption = transaction.EXEMPTION.HasValue ? transaction.EXEMPTION.Value : 0;

                if (exemption > transaction.AMOUNT)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisBill_TongSoTienMienGiamVaQuyChiTraKhongDuocLonHonTongSoTienPhaiThanhToanDichVu);
                    return false;
                }
            }
            return true;
        }

        internal bool IsGetNumOrderFromOldSystem(HIS_TRANSACTION data, V_HIS_ACCOUNT_BOOK accountBook)
        {
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER != Constant.IS_TRUE)
                {
                    return true;
                }

                if (data.NUM_ORDER <= 0 && OldSystemCFG.INTEGRATION_TYPE != OldSystemCFG.IntegrationType.PMS)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongDuocDeTrong);
                    return false;
                }

                if (OldSystemCFG.INTEGRATION_TYPE == OldSystemCFG.IntegrationType.PMS)
                {
                    if (String.IsNullOrWhiteSpace(accountBook.TEMPLATE_CODE))
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongCoMauSo, accountBook.ACCOUNT_BOOK_NAME);
                        return false;
                    }

                    long? num = new OldSystem.HMS.InvoiceConsumer(OldSystemCFG.ADDRESS).Get(accountBook.TEMPLATE_CODE);
                    if (!num.HasValue)
                    {
                        MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongLayDuocSoHoaDonTuPMS);
                        return false;
                    }
                    data.NUM_ORDER = num.Value;
                }

                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

        /// <summary>
        /// Can tach, ko de chung trong ham "run" o tren do ham o tren co cho phep client goi de check
        /// truoc khi goi sang he thong the de thanh toan (tai thoi diem nay thi chua co thong tin
        /// giao dich bang the: TIG_TRANSACTION_CODE)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsValidCardTransaction(HIS_TRANSACTION data)
        {
            bool result = true;
            try
            {
                if (data.PAY_FORM_ID == IMSys.DbConfig.HIS_RS.HIS_PAY_FORM.ID__THE
                    && string.IsNullOrWhiteSpace(data.TIG_TRANSACTION_CODE))
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("Thanh toan su dung the bat buoc phai co thong tin TIG_TRANSACTION_CODE.");
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }

        internal bool CheckIsForOtherSale(V_HIS_ACCOUNT_BOOK accountBook)
        {
            try
            {
                if (accountBook.IS_FOR_OTHER_SALE != Constant.IS_TRUE)
                {
                    BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    throw new Exception("So thu chi khong cho phep thanh toan khac IS_FOR_OTHER_SALE != 1");
                }
                return true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return false;
            }
        }

    }
}
