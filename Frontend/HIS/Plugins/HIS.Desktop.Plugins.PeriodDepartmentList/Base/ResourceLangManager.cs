using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodDepartmentList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCPeriodDepartmentList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCPeriodDepartmentList = new ResourceManager("HIS.Desktop.Plugins.PeriodDepartmentList.Resources.Lang", typeof(HIS.Desktop.Plugins.PeriodDepartmentList.UCPeriodDepartmentList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
