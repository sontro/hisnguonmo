using Inventec.Core;
using Inventec.Common.Logging;
using MOS.MANAGER.Base;
using MOS.EFMODEL.DataModels;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.CancelInvoice
{
    class HisTransactionCancelInvoiceCheck : BusinessBase
    {
        internal HisTransactionCancelInvoiceCheck()
            : base()
        {
        }

        internal HisTransactionCancelInvoiceCheck(CommonParam paramCheck)
            : base(paramCheck)
        {
        }

        internal bool VerifyRequireField(CancelInvoiceSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TransactionId <= 0) throw new ArgumentNullException("data.TransactionId");
                if (data.CancelTime <= 0) throw new ArgumentNullException("data.CancelTime");
            }
            catch (ArgumentNullException ex)
            {
                BugUtil.SetBugCode(param, LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
                LogSystem.Warn(ex);
                valid = false;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

        internal bool IsValid(HIS_TRANSACTION data)
        {
            bool valid = true;
            try
            {
                if (IsNotNull(data))
                {
                    if (data.TRANSACTION_TYPE_ID != IMSys.DbConfig.HIS_RS.HIS_TRANSACTION_TYPE.ID__TT)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichKhongPhaiLaThanhToan, data.TRANSACTION_CODE);
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(data.INVOICE_CODE))
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichChuaPhatHanhHoaDonDienTu, data.TRANSACTION_CODE);
                        return false;
                    }
                    if (data.IS_CANCEL_EINVOICE.HasValue && data.IS_CANCEL_EINVOICE.Value == Constant.IS_TRUE)
                    {
                        MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaHuyHoaDonDienTu, data.TRANSACTION_CODE);
                        return false;
                    }
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
