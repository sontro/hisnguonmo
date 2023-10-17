using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Exam.Register.Deposit
{
    class DepositCheck : BusinessBase
    {
        internal DepositCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TRANSACTION transaction, V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNull(transaction);
                valid = valid && checker.HasNotFinancePeriod(transaction);
                valid = valid && this.IsUnlockAccountBook(accountBook);
                valid = valid && checker.IsValidNumOrder(transaction, accountBook);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
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
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
                param.HasException = true;
            }
            return valid;
        }

    }
}
