using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCTreatmentList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCTreatmentList = new ResourceManager("HIS.Desktop.Plugins.TreatmentList.Resources.Lang", typeof(HIS.Desktop.Plugins.TreatmentList.UCTreatmentList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
