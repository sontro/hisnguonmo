using MOS.DAO.Base;
using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.DAO.HisCacheMonitor
{
    partial class HisCacheMonitorGet : EntityBase
    {
        public Dictionary<string, HIS_CACHE_MONITOR> GetDicByCode(HisCacheMonitorSO search, CommonParam param)
        {
            Dictionary<string, HIS_CACHE_MONITOR> dic = new Dictionary<string, HIS_CACHE_MONITOR>();
            try
            {
                List<HIS_CACHE_MONITOR> listRecord = Get(search, param);
                if (listRecord != null)
                {
                    foreach (var item in listRecord)
                    {
                        if (!dic.ContainsKey(item.CACHE_MONITOR_CODE))
                        {
                            dic.Add(item.CACHE_MONITOR_CODE, item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => search), search) + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => param), param), LogType.Error);
                LogSystem.Error(ex);
                dic.Clear();
            }
            return dic;
        }
    }
}
