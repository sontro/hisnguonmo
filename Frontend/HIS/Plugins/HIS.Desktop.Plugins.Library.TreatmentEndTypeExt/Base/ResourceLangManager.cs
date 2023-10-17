using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTreatmentEndTypeExt { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
               // LanguageUCTreatmentEndTypeExt = new ResourceManager("HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Resources.Lang", typeof(HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Run.UCTreatmentEndTypeExt).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
