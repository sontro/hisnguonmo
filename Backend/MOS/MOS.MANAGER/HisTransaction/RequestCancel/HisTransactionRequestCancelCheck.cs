using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.RequestCancel
{
    class HisTransactionRequestCancelCheck: BusinessBase
    {
        internal HisTransactionRequestCancelCheck()
            : base()
        {

        }

        internal HisTransactionRequestCancelCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra du lieu da bi huy hay chua
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool IsAllow(HisTransactionRequestCancelSDO sdo, V_HIS_TRANSACTION transaction)
        {
            bool valid = true;
            try
            {
                if (transaction == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                    LogSystem.Warn("transactionId ko hop le");
                    return false;
                }

                if (transaction.IS_CANCEL == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisTransaction_GiaoDichDaBiHuy);
                    return false;
                }

                if (transaction.CANCEL_REQ_STT == IMSys.DbConfig.HIS_RS.CANCEL_REQ_STT.ID__CANCEL_REQ)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.DaTonTaiYeuCauHuyGiaoDich);
                    return false;
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

        internal bool VerifyRequireField(HisTransactionRequestCancelSDO data)
        {
            bool valid = true;
            try
            {
                if (data == null) throw new ArgumentNullException("data");
                if (data.TransactionId <= 0) throw new ArgumentNullException("data.TransactionId");
                if (data.WorkingRoomId <= 0) throw new ArgumentNullException("data.WorkingRoomId");
                if (string.IsNullOrWhiteSpace(data.CancelReqReason)) throw new ArgumentNullException("data.CancelReqReason");
            }
            catch (ArgumentNullException ex)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.ThieuThongTinBatBuoc);
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
    }
}
