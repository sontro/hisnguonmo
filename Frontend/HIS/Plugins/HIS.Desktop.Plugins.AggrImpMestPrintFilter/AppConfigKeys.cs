
using System;
using System.Collections.Generic;
using System.Linq;

namespace HIS.Desktop.Plugins.AggrImpMestPrintFilter
{
    internal class AppConfigKeys
    {
        #region Public key
        internal const string CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN = "CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN";
        internal const string CONFIG_KEY__HIS_DESKTOP__CHE_DO_IN_GOP_PHIEU_TRA = "CONFIG_KEY__CHE_DO_IN_GOP_PHIEU_TRA";
        internal const string OderOption = "HIS.Desktop.Plugins.AggrExpMest.OderOption";
        #endregion

        internal static List<string> ListParentMedicine
        {
            get
            {
                return ProcessListParentConfig();
            }
        }

        private static List<string> ProcessListParentConfig()
        {
            List<string> result = null;
            try
            {
                string code = HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("HIS.Desktop.Plugins.AggrExpMestPrint.ParentMety");
                if (!String.IsNullOrWhiteSpace(code))
                {
                    result = code.Split(',').ToList();
                }
            }
            catch (Exception ex)
            {
                result = null;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }

        // HIS.Desktop.Plugins.AggrExpMest.OderOption

        internal static Int64 ProcessOderOption
        {
            get
            {
                return ProcessOderOptionConfig();
            }
        }

        private static Int64 ProcessOderOptionConfig()
        {
            Int64 result;
            try
            {
                
                result = Inventec.Common.TypeConvert.Parse.ToInt64(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(OderOption));
                
            }
            catch (Exception ex)
            {
                result = 9999 ;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return result;
        }
    }
}
