using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisUserAccountBook
{
    partial class HisUserAccountBookDelete : BusinessBase
    {
        internal HisUserAccountBookDelete()
            : base()
        {

        }

        internal HisUserAccountBookDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_USER_ACCOUNT_BOOK data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                valid = valid && IsNotNull(data);
                HIS_USER_ACCOUNT_BOOK raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisUserAccountBookDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisUserAccountBookCheck checker = new HisUserAccountBookCheck(param);
                List<HIS_USER_ACCOUNT_BOOK> listRaw = new List<HIS_USER_ACCOUNT_BOOK>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisUserAccountBookDAO.DeleteList(listData);
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
