using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;

namespace HIS.Desktop.Plugins.SereServTemplate.SereServTemplate
{
    class SereServTemplateBehavior : Tool<IDesktopToolContext>, ISereServTemplate
    {
        object[] entity;
        internal SereServTemplateBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object ISereServTemplate.Run()
        {
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            List<long> SERVICE_TYPE_IDs = null;
            long service_id = 0;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is long)
                            service_id = (long)item;
                        else if (item is List<long>)
                            SERVICE_TYPE_IDs = (List<long>)item;
                    }
                }
                if (moduleData != null)
                {
                    return new FormSereServTemplate(moduleData, service_id, SERVICE_TYPE_IDs);
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
