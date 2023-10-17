using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.LocalCacheManager;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.LocalCacheManager.LocalCacheManager
{
    public sealed class LocalCacheManagerBehavior : Tool<IDesktopToolContext>, ILocalCacheManager
    {              
        Inventec.Desktop.Common.Modules.Module moduleData;

        public LocalCacheManagerBehavior()
            : base()
        {
        }

        public LocalCacheManagerBehavior(CommonParam param,  Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {         
            this.moduleData = moduleData;
        }

        object ILocalCacheManager.Run()
        {
            try
            {
                return new frmLocalCacheManager(this.moduleData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
