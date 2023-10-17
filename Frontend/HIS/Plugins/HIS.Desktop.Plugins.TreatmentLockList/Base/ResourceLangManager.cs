using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentLockList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTreatmentLockList { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTreatmentLockList = new ResourceManager("HIS.Desktop.Plugins.TreatmentLockList.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentLockList.frmTreatmentLockList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
