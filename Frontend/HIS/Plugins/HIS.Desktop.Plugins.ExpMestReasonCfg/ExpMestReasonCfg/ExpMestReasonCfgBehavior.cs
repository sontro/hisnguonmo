using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestReasonCfg
{
    class ExpMestReasonCfgBehavior : Tool<IDesktopToolContext>, IExpMestReasonCfg
    {
        object[] entity;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        internal ExpMestReasonCfgBehavior()
            : base()
        {

        }

        internal ExpMestReasonCfgBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IExpMestReasonCfg.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null)
                {
                    return new frmExpMestReasonCfg(moduleData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
