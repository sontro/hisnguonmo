using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestPay.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmBorderau { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmBorderau = new ResourceManager("HIS.Desktop.Plugins.ImpMestPay.Resources.Lang", typeof(HIS.Desktop.Plugins.ImpMestPay.frmImpMestPay).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
