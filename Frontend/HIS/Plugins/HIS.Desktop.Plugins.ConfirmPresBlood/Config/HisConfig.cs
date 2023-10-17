using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConfirmPresBlood.Config
{
    public class HisConfig
    {
        private static string BCS_APPROVE_OTHER_TYPE_IS_ALLOW = "MOS.HIS_EXP_MEST.BCS.APPROVE_OTHER_TYPE.IS_ALLOW";
        private static string BCS_APPROVE_IS_AUTO_REPLACE = "HIS.HIS_EXP_MEST.BCS.APPROVE.IS_AUTO_REPLACE";
        private static string DONT_PRES_EXPIRED_ITEM = "MOS.HIS_MEDI_STOCK.DONT_PRES_EXPIRED_ITEM";

        internal static string IS_ALLOW_REPLACE;
        internal static string IS_AUTO_REPLACE;
        internal static bool IS_DONT_PRES_EXPIRED_ITEM { get; set; }

        internal static void LoadConfig()
        {
            try
            {
                IS_ALLOW_REPLACE = HisConfigs.Get<string>(BCS_APPROVE_OTHER_TYPE_IS_ALLOW);
                IS_AUTO_REPLACE = HisConfigs.Get<string>(BCS_APPROVE_IS_AUTO_REPLACE);
                IS_DONT_PRES_EXPIRED_ITEM = HisConfigs.Get<string>(DONT_PRES_EXPIRED_ITEM) == "1";
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
