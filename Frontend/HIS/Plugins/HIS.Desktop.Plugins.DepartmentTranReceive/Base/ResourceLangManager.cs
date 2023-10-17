using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DepartmentTranReceive.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCDepartmentTranReceive { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCDepartmentTranReceive = new ResourceManager("HIS.Desktop.Plugins.DepartmentTranReceive.Resources.Lang", typeof(HIS.Desktop.Plugins.DepartmentTranReceive.UCDepartmentTranReceive).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
