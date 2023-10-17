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
    public class HisAggrImpMestLog
    {
        public static void Run(List<HIS_IMP_MEST> aggrImpMests, List<HIS_IMP_MEST> children, EventLog.Enum logEnum)
        {
            try
            {
                new EventLogGenerator(logEnum).AggrImpMestList(GetAggrImpMestData(aggrImpMests, children)).Run();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }

        private static List<AggrImpMestData> GetAggrImpMestData(List<HIS_IMP_MEST> aggrImpMests, List<HIS_IMP_MEST> children)
        {
            try
            {
                List<AggrImpMestData> aggrImpMestData = new List<AggrImpMestData>();
                if (aggrImpMests != null && aggrImpMests.Count > 0
                    && children != null && children.Count > 0)
                {
                    foreach (HIS_IMP_MEST aggr in aggrImpMests)
                    {
                        List<string> imps = children
                            .Where(o => o.AGGR_IMP_MEST_ID == aggr.ID)
                            .Select(o => o.IMP_MEST_CODE)
                            .ToList();
                        if (children != null)
                        {
                            aggrImpMestData.Add(new AggrImpMestData(aggr.IMP_MEST_CODE, imps));
                        }
                    }
                    return aggrImpMestData;
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
