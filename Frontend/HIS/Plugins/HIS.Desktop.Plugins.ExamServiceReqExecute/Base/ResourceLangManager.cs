using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExamServiceReqExecute { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExamServiceReqExecute = new ResourceManager("HIS.Desktop.Plugins.ExamServiceReqExecute.Resources.Lang", typeof(HIS.Desktop.Plugins.ExamServiceReqExecute.ExamServiceReqExecuteControl).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
