using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisAccountBook.Authority;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Billing
{
    class TransactionBillCheck : BusinessBase
    {
        internal TransactionBillCheck()
            : base()
        {

        }

        internal TransactionBillCheck(CommonParam param)
            : base(param)
        {

        }

        /// <summary>
        /// Kiem tra thuc hien giao dich thanh toan (Phuc vu giao dich bang the onelink)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        internal bool Run(HisTransactionBillSDO data, V_HIS_ACCOUNT_BOOK hisAccountBook)
        {
            bool valid = true;
            try
            {
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && IsNotNull(data.Transaction);
                valid = valid && IsNotNullOrEmpty(data.SereServBills);
                valid = valid && this.IsAuthorized(data.Transaction, data.RequestRoomId);
                valid = valid && checker.IsUnlockAccountBook(data.Transaction.ACCOUNT_BOOK_ID, ref hisAccountBook);
                valid = valid && checker.HasNotFinancePeriod(data.Transaction);
                valid = valid && this.IsUnlockAccountBook(hisAccountBook);
                valid = valid && checker.IsValidNumOrder(data.Transaction, hisAccountBook);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsUnlockAccountBook(V_HIS_ACCOUNT_BOOK hisAccountBook)
        {
            bool valid = true;
            try
            {
                if (hisAccountBook == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    return false;
                }

                if (hisAccountBook.IS_ACTIVE == null || hisAccountBook.IS_ACTIVE.Value != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, hisAccountBook.ACCOUNT_BOOK_NAME);
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

        /// <summary>
        /// Kiem tra xem tai khoan co duoc uy quyen boi tai khoan dang lam viec o phong thu ngan ko
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="workingRoomId"></param>
        /// <returns></returns>
        private bool IsAuthorized(HIS_TRANSACTION transaction, long workingRoomId)
        {
            V_HIS_CASHIER_ROOM cashierRoom = HisCashierRoomCFG.DATA.Where(o => o.ID == transaction.CASHIER_ROOM_ID).FirstOrDefault();
            if (cashierRoom == null)
            {
                MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.DuLieuDauVaoKhongHopLe);
                LogSystem.Warn("CASHIER_ROOM_ID ko hop le");
                return false;
            }
            if (!new HisAccountBookAuthorityProcessor(param).IsAuthorized(transaction.CASHIER_LOGINNAME, transaction.CASHIER_USERNAME, cashierRoom.ROOM_ID, transaction.ACCOUNT_BOOK_ID, workingRoomId))
            {
                return false;
            }
            return true;
        }
    }
}
