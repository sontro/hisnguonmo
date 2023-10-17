using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestAggregate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCExpMestAggregate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCExpMestAggregate = new ResourceManager("HIS.Desktop.Plugins.ExpMestAggregate.Resources.Lang", typeof(HIS.Desktop.Plugins.ExpMestAggregate.UCExpMestAggregate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
