using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisCaroAccountBook;
using MOS.MANAGER.HisUserAccountBook;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisAccountBook
{
    partial class HisAccountBookTruncate : BusinessBase
    {
        internal HisAccountBookTruncate()
            : base()
        {

        }

        internal HisAccountBookTruncate(CommonParam paramTruncate)
            : base(paramTruncate)
        {

        }

        internal bool Truncate(HIS_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisAccountBookCheck checker = new HisAccountBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.VerifyTransactionExistForDelete(raw);
                if (valid)
                {
                    result = new HisUserAccountBookTruncate(param).TruncateByAccountBookId(data.ID)
                        && new HisCaroAccountBookTruncate(param).TruncateByAccountBookId(data.ID)
                        && DAOWorker.HisAccountBookDAO.Truncate(data);
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = false;
            }
            return result;
        }
    }
}
