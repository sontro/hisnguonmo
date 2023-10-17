using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleEdit.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestSaleEdit { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestSaleEdit = new ResourceManager("HIS.Desktop.Plugins.ExpMestSaleEdit.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestSaleEdit.UCExpMestSaleEdit).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
