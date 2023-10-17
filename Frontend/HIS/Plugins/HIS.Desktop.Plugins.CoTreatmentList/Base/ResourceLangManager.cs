using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CoTreatmentList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCDepartmentTranReceive { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCDepartmentTranReceive = new ResourceManager("HIS.Desktop.Plugins.CoTreatmentList.Resources.Lang", typeof(HIS.Desktop.Plugins.CoTreatmentList.UCCoTreatmentList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
