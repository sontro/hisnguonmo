using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestSaleCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestSaleCreate = new ResourceManager("HIS.Desktop.Plugins.ExpMestSaleCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestSaleCreate.UCExpMestSaleCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
