using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLiquExpMestCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCAllocateExpeExpMestCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCAllocateExpeExpMestCreate = new ResourceManager("HIS.Desktop.Plugins.AllocateLiquExpMestCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.AllocateLiquExpMestCreate.UCAllocateLiquExpMestCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
