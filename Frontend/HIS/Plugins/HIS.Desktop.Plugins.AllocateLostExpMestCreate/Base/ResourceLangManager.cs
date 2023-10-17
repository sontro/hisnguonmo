using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AllocateLostExpMestCreate.Base
{
    class ResourceLangManager
    {
        internal static ResourceManager LanguageUCAllocateExpeExpMestCreate { get; set; }
        internal static void InitResourceLanguageManager()
        {
            try
            {
                LanguageUCAllocateExpeExpMestCreate = new ResourceManager("HIS.Desktop.Plugins.AllocateLostExpMestCreate.Resources.Lang", typeof(HIS.Desktop.Plugins.AllocateLostExpMestCreate.UCAllocateLostExpMestCreate).Assembly);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
