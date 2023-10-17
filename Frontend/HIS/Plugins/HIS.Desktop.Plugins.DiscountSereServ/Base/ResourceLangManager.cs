using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmDiscountSereServ { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmDiscountSereServ = new ResourceManager("HIS.Desktop.Plugins.DiscountSereServ.Resources.Lang", typeof(HIS.Desktop.Plugins.DiscountSereServ.frmDiscountSereServ).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
