using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMachineServMaty.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCDepartmentTranReceive { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCDepartmentTranReceive = new ResourceManager("HIS.Desktop.Plugins.HisMachineServMaty.Resources.Lang", typeof(HIS.Desktop.Plugins.HisMachineServMaty.UCHisMachineServMaty).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
