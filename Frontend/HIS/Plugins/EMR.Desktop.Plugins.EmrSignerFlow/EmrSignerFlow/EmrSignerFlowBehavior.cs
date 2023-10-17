using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignerFlow.EmrSignerFlow
{
    class EmrSignerFlowBehavior : Tool<IDesktopToolContext>, IEmrSignerFlow
    {
        object[] entity;        
        Inventec.Desktop.Common.Modules.Module currentModule;

        internal EmrSignerFlowBehavior()
            : base()
        {

        }

        internal EmrSignerFlowBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IEmrSignerFlow.Run()
        {
            object result = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    result = new UC_EmrSignerFlow();
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }
    }
}
