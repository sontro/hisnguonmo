using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RehaServiceReqExecute.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCRehaServiceReqExecute { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCRehaServiceReqExecute = new ResourceManager("HIS.Desktop.Plugins.RehaServiceReqExecute.Resources.Lang", typeof(HIS.Desktop.Plugins.RehaServiceReqExecute.RehaServiceReqExecuteControl).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
