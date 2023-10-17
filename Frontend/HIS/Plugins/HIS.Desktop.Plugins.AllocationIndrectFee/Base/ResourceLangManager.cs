using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocationIndrectFee.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmAllocationIndrectFee { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmAllocationIndrectFee = new ResourceManager("HIS.Desktop.Plugins.AllocationIndrectFee.Resources.Lang", typeof(HIS.Desktop.Plugins.AllocationIndrectFee.frmAllocationIndrectFee).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
