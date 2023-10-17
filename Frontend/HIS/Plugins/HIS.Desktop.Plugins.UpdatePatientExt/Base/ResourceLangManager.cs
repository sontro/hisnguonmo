using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdatePatientExt.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCUpdatePatientExt { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCUpdatePatientExt = new ResourceManager("HIS.Desktop.Plugins.UpdatePatientExt.Resources.Lang", typeof(HIS.Desktop.Plugins.UpdatePatientExt.frmUpdatePatientExt).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
