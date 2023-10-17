using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAccountBook
{
    class HisAccountBookUpdate : BusinessBase
    {
        internal HisAccountBookUpdate()
            : base()
        {

        }

        internal HisAccountBookUpdate(CommonParam paramUpdate)
            : base(paramUpdate)
        {

        }

        internal bool Update(HIS_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HIS_ACCOUNT_BOOK raw = null;
                HisAccountBookCheck checker = new HisAccountBookCheck(param);
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.VerifyBookType(data); 
                valid = valid && checker.VerifyRequireField(data);
                valid = valid && checker.VerifyTransactionExistForUpdate(data, raw);
                valid = valid && checker.IsUnLock(raw);
                valid = valid && checker.ExistsCode(data.ACCOUNT_BOOK_CODE, data.ID);
                if (valid)
                {
                    result = DAOWorker.HisAccountBookDAO.Update(data);
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

        private bool UpdateList(List<HIS_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisAccountBookCheck checker = new HisAccountBookCheck(param);
                foreach (var data in listData)
                {
                    valid = valid && checker.VerifyRequireField(data);
                    valid = valid && IsGreaterThanZero(data.ID);
                    valid = valid && checker.IsUnLock(data.ID);
                    valid = valid && checker.ExistsCode(data.ACCOUNT_BOOK_CODE, data.ID);
                }
                if (valid)
                {
                    result = DAOWorker.HisAccountBookDAO.UpdateList(listData);
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
