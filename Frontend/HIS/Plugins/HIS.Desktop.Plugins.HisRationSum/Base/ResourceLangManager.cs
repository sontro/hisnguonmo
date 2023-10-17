using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisRationSum.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCHisRationSum { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCHisRationSum = new ResourceManager("HIS.Desktop.Plugins.HisRationSum.Resources.Lang", typeof(HIS.Desktop.Plugins.HisRationSum.UCHisRationSum).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
