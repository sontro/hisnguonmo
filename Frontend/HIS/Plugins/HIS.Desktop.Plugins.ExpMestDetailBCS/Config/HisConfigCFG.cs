using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestDetailBCS.Config
{
    class HisConfigCFG
    {
        internal const string BCS_APPROVE_OTHER_TYPE_IS_ALLOW = "MOS.HIS_EXP_MEST.BCS.APPROVE_OTHER_TYPE.IS_ALLOW";
        private const string CONFIG_KEY__IS_JOIN_NAME_WITH_CONCENTRA = "HIS.HIS_MEDI_STOCK.IS_JOIN_NAME_WITH_CONCENTRA";//Noi ten thuoc vs ham luong

        internal static bool IS_JOIN_NAME_WITH_CONCENTRA;

        internal static void LoadConfig()
        {
            try
            {
                IS_JOIN_NAME_WITH_CONCENTRA = GetValue(CONFIG_KEY__IS_JOIN_NAME_WITH_CONCENTRA) == "1";
                Inventec.Common.Logging.LogSystem.Debug("IS_JOIN_NAME_WITH_CONCENTRA: " + IS_JOIN_NAME_WITH_CONCENTRA);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                return HisConfigs.Get<string>(code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
                result = null;
            }
            return result;
        }
    }
}
