using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrMobaImpMests.Config
{
    class HisConfig
    {
        private const string CONFIG_KEY__ODD_MANAGER_OPTION = "MOS.HIS_IMP_MEST.AGGR_MOBA_PRES.ODD_MANAGER_OPTION";

        internal enum OddOption
        {
            DEFAULT = 0,
            ADDICTIVE_PSYCHOACITVE = 1,
        }

        internal static int OddManagerOption;

        internal static void LoadConfig()
        {
            try
            {
                OddManagerOption = HisConfigs.Get<int>(CONFIG_KEY__ODD_MANAGER_OPTION);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
