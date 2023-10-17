using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestAggregate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCImpMestAggregate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCImpMestAggregate = new ResourceManager("HIS.Desktop.Plugins.ImpMestAggregate.Resources.Lang", typeof(HIS.Desktop.Plugins.ImpMestAggregate.UCImpMestAggregate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
