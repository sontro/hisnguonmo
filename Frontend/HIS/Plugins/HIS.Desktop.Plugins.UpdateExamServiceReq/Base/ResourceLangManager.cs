using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdateExamServiceReq.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCUpdateExamServiceReq { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCUpdateExamServiceReq = new ResourceManager("HIS.Desktop.Plugins.UpdateExamServiceReq.Resources.Lang", typeof(HIS.Desktop.Plugins.UpdateExamServiceReq.frmUpdateExamServiceReq).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
