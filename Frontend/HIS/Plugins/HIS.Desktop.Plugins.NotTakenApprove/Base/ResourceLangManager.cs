using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.NotTakenApprove.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LangFrmNotTakenApprove { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LangFrmNotTakenApprove = new ResourceManager("HIS.Desktop.Plugins.NotTakenApprove.Resources.Lang", typeof(HIS.Desktop.Plugins.NotTakenApprove.frmNotTakenApprove).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
