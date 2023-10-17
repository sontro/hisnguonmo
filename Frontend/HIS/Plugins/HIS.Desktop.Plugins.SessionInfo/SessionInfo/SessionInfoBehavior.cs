using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SessionInfo.SessionInfo
{
    class SessionInfoBehavior : Tool<IDesktopToolContext>, ISessionInfo
    {
        Inventec.Desktop.Common.Modules.Module Module;
        public SessionInfoBehavior()
            : base()
        {
        }

        public SessionInfoBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param)
            : base()
        {
            this.Module = module;
        }

        object ISessionInfo.Run()
        {
            try
            {
                return new frmSessionInfo(this.Module);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
