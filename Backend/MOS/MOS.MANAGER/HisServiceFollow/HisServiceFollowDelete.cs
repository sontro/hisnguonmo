using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisServiceFollow
{
    partial class HisServiceFollowDelete : BusinessBase
    {
        internal HisServiceFollowDelete()
            : base()
        {

        }

        internal HisServiceFollowDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_SERVICE_FOLLOW data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisServiceFollowCheck checker = new HisServiceFollowCheck(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_FOLLOW raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisServiceFollowDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_SERVICE_FOLLOW> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisServiceFollowCheck checker = new HisServiceFollowCheck(param);
                List<HIS_SERVICE_FOLLOW> listRaw = new List<HIS_SERVICE_FOLLOW>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisServiceFollowDAO.DeleteList(listData);
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
