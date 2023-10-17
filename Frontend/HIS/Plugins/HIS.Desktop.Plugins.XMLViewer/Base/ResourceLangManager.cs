using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.XMLViewer.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmTransactionDepositCancel { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmTransactionDepositCancel = new ResourceManager("HIS.Desktop.Plugins.XMLViewer.Resources.Lang", typeof(HIS.Desktop.Plugins.XMLViewer.frmXMLViewer).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
