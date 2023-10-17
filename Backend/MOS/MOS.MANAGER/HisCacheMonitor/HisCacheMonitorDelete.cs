using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.HisCacheMonitor
{
    partial class HisCacheMonitorDelete : BusinessBase
    {
        internal HisCacheMonitorDelete()
            : base()
        {

        }

        internal HisCacheMonitorDelete(CommonParam paramDelete)
            : base(paramDelete)
        {

        }

        internal bool Delete(HIS_CACHE_MONITOR data)
        {
            bool result = false;
            try
            {
                bool valid = true;
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                valid = valid && IsNotNull(data);
                HIS_CACHE_MONITOR raw = null;
                valid = valid && checker.VerifyId(data.ID, ref raw);
                valid = valid && checker.IsUnLock(raw);
                if (valid)
                {
                    result = DAOWorker.HisCacheMonitorDAO.Delete(data);
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

        internal bool DeleteList(List<HIS_CACHE_MONITOR> listData)
        {
            bool result = false;
            try
            {
                bool valid = true;
                valid = IsNotNullOrEmpty(listData);
                HisCacheMonitorCheck checker = new HisCacheMonitorCheck(param);
                List<HIS_CACHE_MONITOR> listRaw = new List<HIS_CACHE_MONITOR>();
                List<long> listId = listData.Select(o => o.ID).ToList();
                valid = valid && checker.VerifyIds(listId, listRaw);
                valid = valid && checker.IsUnLock(listRaw);
                if (valid)
                {
                    result = DAOWorker.HisCacheMonitorDAO.DeleteList(listData);
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
