using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PayClinicalResult.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExecuteRoom { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExecuteRoom = new ResourceManager("HIS.Desktop.Plugins.PayClinicalResult.Resources.Lang", typeof(HIS.Desktop.Plugins.PayClinicalResult.UCExecuteRoom).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
