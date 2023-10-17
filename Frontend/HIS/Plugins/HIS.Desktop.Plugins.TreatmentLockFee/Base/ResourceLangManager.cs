using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentLockFee.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTreatmentLockFee { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTreatmentLockFee = new ResourceManager("HIS.Desktop.Plugins.TreatmentLockFee.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentLockFee.frmTreatmentLockFee).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
