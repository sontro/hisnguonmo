using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockInventory.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageFrmMobaImpMestCreate { get; set; }

        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageFrmMobaImpMestCreate = new ResourceManager("HIS.Desktop.Plugins.MediStockInventory.Resources.Lang", typeof(HIS.Desktop.Plugins.MediStockInventory.frmMediStockInventory).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
