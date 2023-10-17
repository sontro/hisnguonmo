using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisSereServ;
using MOS.MANAGER.HisSeseTransReq;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.PaylaterDeposit
{
    class HisTransReqDepositCheck : BusinessBase
    {
        internal HisTransReqDepositCheck()
            : base()
        {

        }

        internal HisTransReqDepositCheck(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HisTransReqDepositData data, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                HisTreatmentCheck treatmentChecker = new HisTreatmentCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && this.IsUnlockAccountBook(data.AccountBookId, ref accountBook);
                valid = valid && this.IsGeneralOrder(accountBook);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                valid = false;
            }
            return valid;
        }

        internal bool IsUnlockAccountBook(long accountBookId, ref V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                accountBook = new HisAccountBookGet().GetViewById(accountBookId);

                if (accountBook == null)
                {
                    MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.KXDDDuLieuCanXuLy);
                    LogSystem.Warn("So thu chi khong ton tai tren he thong. accountBookId: " + accountBookId);
                    return false;
                }

                if (accountBook.IS_ACTIVE == null || accountBook.IS_ACTIVE.Value != IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoDangBiKhoa, accountBook.ACCOUNT_BOOK_NAME);
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

        bool IsGeneralOrder(V_HIS_ACCOUNT_BOOK accountBook)
        {
            bool valid = true;
            try
            {
                if (accountBook.IS_NOT_GEN_TRANSACTION_ORDER.HasValue && accountBook.IS_NOT_GEN_TRANSACTION_ORDER.Value == Constant.IS_TRUE)
                {
                    MOS.MANAGER.Base.MessageUtil.SetMessage(param, MOS.LibraryMessage.Message.Enum.HisAccountBook_SoBienLaiKhongTuDongTangSo, accountBook.ACCOUNT_BOOK_NAME);
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
