using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqList.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmServiceReqList { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmServiceReqList = new ResourceManager("HIS.Desktop.Plugins.ServiceReqList.Resources.Lang", typeof(HIS.Desktop.Plugins.ServiceReqList.frmServiceReqList).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
