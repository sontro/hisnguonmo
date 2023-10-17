using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCImpMestCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCImpMestCreate = new ResourceManager("HIS.Desktop.Plugins.ImpMestCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.ImpMestCreate.UCImpMestCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
