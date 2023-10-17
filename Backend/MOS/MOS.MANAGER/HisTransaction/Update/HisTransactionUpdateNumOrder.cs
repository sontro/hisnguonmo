using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.MANAGER.EventLogUtil;
using MOS.MANAGER.HisAccountBook;
using MOS.MANAGER.HisEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisTransaction.Update
{
    class HisTransactionUpdateNumOrder : BusinessBase
    {
        internal HisTransactionUpdateNumOrder()
            : base()
        {

        }

        internal HisTransactionUpdateNumOrder(CommonParam param)
            : base(param)
        {

        }

        internal bool Run(HIS_TRANSACTION data, ref HIS_TRANSACTION resultData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_TRANSACTION raw = null;
                V_HIS_ACCOUNT_BOOK accountBook = null;
                HisTransactionCheck checker = new HisTransactionCheck(param);
                valid = valid && IsNotNull(data);
                valid = valid && this.CheckConfigAllow();
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnCancel(raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.IsNotCollect(raw);                
                valid = valid && checker.IsUnlockAccountBook(raw.ACCOUNT_BOOK_ID, ref accountBook);
                valid = valid && checker.IsNotGenTransactionNumOrder(accountBook);
                valid = valid && this.CheckNumOrder(data, raw);
                valid = valid && checker.IsInRangeNumOrder(accountBook, data.NUM_ORDER);
                if (valid)
                {
                    long oldNum = raw.NUM_ORDER;
                    raw.NUM_ORDER = data.NUM_ORDER;
                    if (!DAOWorker.HisTransactionDAO.Update(raw))
                    {
                        MOS.MANAGER.Base.BugUtil.SetBugCode(param, MOS.LibraryBug.Bug.Enum.HisTransaction_ThemMoiThatBai);
                        throw new Exception("update NUM_ORDER HisTransaction that bai." + LogUtil.TraceData("raw", raw));
                    }
                    result = true;
                    resultData = raw;
                    new EventLogGenerator(LibraryEventLog.EventLog.Enum.HisTransaction_SuaSoBienLai, oldNum, raw.NUM_ORDER).TransactionCode(raw.TRANSACTION_CODE).Run();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }

        private bool CheckConfigAllow()
        {
            bool valid = true;
            try
            {
                if (!HisTransactionCFG.IS_ALLOW_EDIT_NUM_ORDER)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_KhongChoPhepSuSoChungTu);
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

        private bool CheckNumOrder(HIS_TRANSACTION data, HIS_TRANSACTION raw)
        {
            bool valid = true;
            try
            {
                if (raw.NUM_ORDER == data.NUM_ORDER)
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoBienLaiKhongThayDoi);
                    return false;
                }

                HisTransactionFilterQuery filter = new HisTransactionFilterQuery();
                filter.NUM_ORDER__EQUAL = data.NUM_ORDER;
                filter.ACCOUNT_BOOK_ID = raw.ACCOUNT_BOOK_ID;
                var listTran = new HisTransactionGet().Get(filter);
                if (IsNotNullOrEmpty(listTran))
                {
                    MANAGER.Base.MessageUtil.SetMessage(param, LibraryMessage.Message.Enum.HisTransaction_SoChungTuCuaSoThuChiDaTonTai, data.NUM_ORDER.ToString(), "");
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
    }
}
