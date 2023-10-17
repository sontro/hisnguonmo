using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContentSubclinical.Config
{
    class Config
    {
        internal const string OptionMergePrint_IsmergeCFG = "HIS.Desktop.Plugins.OptionMergePrint.Ismerge";

        internal static bool IsmergePrint;

        internal static void LoadConfig()
        {
            try
            {
                IsmergePrint = HisConfigs.Get<string>(OptionMergePrint_IsmergeCFG) == "1";

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
