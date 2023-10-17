using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisSereServExtCFG
    {
        private const string AUTO_GATHER_DATA_SURG_DAY_CFG = "MOS.HIS_SERE_SERV_EXT.AUTO_GATHER_DATA_SURG.DAY";

        private static int? DateAutoGatherDataSurg;
        public static int DATE_AUTO_GATHER_DATA_SURG_DAY
        {
            get
            {
                if (!DateAutoGatherDataSurg.HasValue)
                {
                    DateAutoGatherDataSurg = ConfigUtil.GetIntConfig(AUTO_GATHER_DATA_SURG_DAY_CFG);
                }
                return DateAutoGatherDataSurg.Value;
            }
        }

      
        public static void Reload()
        {
            DateAutoGatherDataSurg = ConfigUtil.GetIntConfig(AUTO_GATHER_DATA_SURG_DAY_CFG);
        }
    }
}
