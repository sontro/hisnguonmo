using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class IntegratedSystemCFG
    {
        private const string INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION_CFG = "MOS.INTEGRATED_SYSTEM.SYNC.INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME.OPTION";

        private static bool? intructionTimeMustBeLessThanSystemTimeOption;
        public static bool INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION
        {
            get
            {
                if (!intructionTimeMustBeLessThanSystemTimeOption.HasValue)
                {
                    intructionTimeMustBeLessThanSystemTimeOption = ConfigUtil.GetIntConfig(INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION_CFG) == 1;
                }
                return intructionTimeMustBeLessThanSystemTimeOption.Value;
            }
        }

        public static void Reload()
        {
            intructionTimeMustBeLessThanSystemTimeOption = ConfigUtil.GetIntConfig(INTRUCTION_TIME_MUST_BE_LESS_THAN_SYSTEM_TIME_OPTION_CFG) == 1;
        }
    }
}
