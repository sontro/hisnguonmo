using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreateV2.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestSaleCreateV2 { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestSaleCreateV2 = new ResourceManager("HIS.Desktop.Plugins.ExpMestSaleCreateV2.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestSaleCreateV2.UCExpMestSaleCreateV2).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
