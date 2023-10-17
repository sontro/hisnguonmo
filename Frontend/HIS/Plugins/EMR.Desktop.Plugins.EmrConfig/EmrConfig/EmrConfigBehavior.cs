using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Desktop.Core;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Common.Modules;

namespace EMR.Desktop.Plugins.EmrConfig.EmrConfig
{
    class EmrConfigBehavior : BusinessBase, IEmrConfig
    {
        object[] Entity;
        internal EmrConfigBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.Entity = filter;
        }
        object IEmrConfig.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module module = null;
                string WorkingModuleLink = "";
                if (this.Entity.GetType() == typeof(object[]))
                {
                    foreach (var item in this.Entity)
                    {
                        if (item is Module)
                        {
                            module = (Module)item;
                        }
                        else if (item is string)
                        {
                            WorkingModuleLink = (string)item;
                        }
                    }
                }

                if (module != null && !string.IsNullOrEmpty(WorkingModuleLink))
                {
                    return new EMR.Desktop.Plugins.EmrConfig.EmrConfig.frmEmrConfig(module, WorkingModuleLink);
                }
                else
                    return new EMR.Desktop.Plugins.EmrConfig.EmrConfig.frmEmrConfig(module);
            }
            catch (Exception ex)
            {

                Inventec.Common.Logging.LogSystem.Warn(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
