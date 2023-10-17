using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicalStore.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCMedicalStore { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCMedicalStore = new ResourceManager("HIS.Desktop.Plugins.MedicalStore.Resources.Lang", typeof(HIS.Desktop.Plugins.MedicalStore.UCMedicalStore).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
