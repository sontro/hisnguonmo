using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCSurgServiceReqExecute { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCSurgServiceReqExecute = new ResourceManager("HIS.Desktop.Plugins.SurgServiceReqExecute.Resources.Lang", typeof(HIS.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecuteControl).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
