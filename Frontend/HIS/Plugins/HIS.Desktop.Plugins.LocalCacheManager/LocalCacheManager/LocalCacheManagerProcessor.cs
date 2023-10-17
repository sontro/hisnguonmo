using Inventec.Core;
using Inventec.Desktop.Core;
using HIS.Desktop.Plugins.LocalCacheManager.LocalCacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Common.Modules;

namespace HIS.Desktop.Plugins.LocalCacheManager
{
    [ExtensionOf(typeof(DesktopRootExtensionPoint),
       "HIS.Desktop.Plugins.LocalCacheManager",
       "Quản lý cache máy của tôi (Code)",
       "Common",
       14,
       "cache.png",
       "A",
       Module.MODULE_TYPE_ID__FORM,
       true,
       true
       )
    ]
    public class LocalCacheManagerProcessor : ModuleBase, IDesktopRoot
    {
        CommonParam param;
        public LocalCacheManagerProcessor()
        {
            param = new CommonParam();
        }
        public LocalCacheManagerProcessor(CommonParam paramBusiness)
        {
            param = (paramBusiness != null ? paramBusiness : new CommonParam());
        }

        public object Run(object[] args)
        {
            object result = null;
            try
            {
                ILocalCacheManager behavior = LocalCacheManagerFactory.MakeILocalCacheManager(param, args);
                result = behavior != null ? (behavior.Run()) : null;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Ham tra ve trang thai cua module la enable hay disable
        /// Ghi de gia tri khac theo nghiep vu tung module
        /// </summary>
        /// <returns>true/false</returns>
        public override bool IsEnable()
        {
            bool result = false;
            try
            {
                result = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }

            return result;
        }
    }
}
