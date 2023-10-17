using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.LogManager;
using MOS.MANAGER.Base;
using MOS.MANAGER.Config;
using MOS.SDO;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    public class HisAggrExpMestLog
    {
        public static void Run(Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> aggrDic, EventLog.Enum logEnum)
        {
            try
            {
                new EventLogGenerator(logEnum).AggrExpMestList(GetAggrExpMestData(aggrDic)).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static List<AggrExpMestData> GetAggrExpMestData(Dictionary<HIS_EXP_MEST, List<HIS_EXP_MEST>> aggrDic)
        {
            try
            {
                List<AggrExpMestData> aggrExpMestData = new List<AggrExpMestData>();
                if (aggrDic != null && aggrDic.Count > 0)
                {
                    foreach (HIS_EXP_MEST exp in aggrDic.Keys)
                    {
                        List<HIS_EXP_MEST> children = aggrDic[exp];
                        if (children != null)
                        {
                            aggrExpMestData.Add(new AggrExpMestData(exp.EXP_MEST_CODE, children.Select(o => o.EXP_MEST_CODE).ToList()));
                        }
                    }
                    return aggrExpMestData;
                }
                return null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

    }
}
