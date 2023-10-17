using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalSurgery.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCApprovalSurgery { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                //LanguageUCApprovalSurgery = new ResourceManager("HIS.Desktop.Plugins.ApprovalSurgery.Resources.Lang", typeof(HIS.Desktop.Plugins.ApprovalSurgery.frmApprovalSurgery).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
