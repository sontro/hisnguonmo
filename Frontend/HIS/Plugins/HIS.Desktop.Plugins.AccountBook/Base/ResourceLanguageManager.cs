using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList.Base
{
    public class ResourceLanguageManager
    {
        public static ResourceManager LanguageUCAccountBook { get; set; }
        public static ResourceManager LanguageFormReportTime { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                ResourceLanguageManager.LanguageUCAccountBook = new ResourceManager("HIS.Desktop.Plugins.HisAccountBookList.Resources.Lang", typeof(HIS.Desktop.Plugins.HisAccountBookList.UCHisAccountBookList).Assembly);
                ResourceLanguageManager.LanguageFormReportTime = new ResourceManager("HIS.Desktop.Plugins.HisAccountBookList.Resources.Lang", typeof(HIS.Desktop.Plugins.HisAccountBookList.FormReportTime).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
