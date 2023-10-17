using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisRationSumCFG
    {
        private const string KEY__AUTO_SPLIT_BY_INTRUCTION_DATE = "MOS.HIS_RATION_SUM.AUTO_SPLIT_BY_INTRUCTION_DATE";

        private static int? autoSplitByIntructionDate;
        public static int? AUTO_SPLIT_BY_INTRUCTION_DATE
        {
            get
            {
                if (!autoSplitByIntructionDate.HasValue)
                {
                    autoSplitByIntructionDate = ConfigUtil.GetIntConfig(KEY__AUTO_SPLIT_BY_INTRUCTION_DATE);
                }
                return autoSplitByIntructionDate;
            }
        }

        public static void Reload()
        {
            autoSplitByIntructionDate = ConfigUtil.GetIntConfig(KEY__AUTO_SPLIT_BY_INTRUCTION_DATE);
        }
    }
}
