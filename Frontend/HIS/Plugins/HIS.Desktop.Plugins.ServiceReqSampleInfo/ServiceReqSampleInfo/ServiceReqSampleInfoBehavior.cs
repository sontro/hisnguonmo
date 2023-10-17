using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqSampleInfo
{
    class ServiceReqSampleInfoBehavior : Tool<IDesktopToolContext>, IServiceReqSampleInfo
    {
        object[] entity;
        long serviceReqId;

        Inventec.Desktop.Common.Modules.Module moduleData = null;
        RefeshReference RefreshData = null;
        internal ServiceReqSampleInfoBehavior()
            : base()
        {

        }

        internal ServiceReqSampleInfoBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IServiceReqSampleInfo.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if (item is long)
                        {
                            serviceReqId = (long)item;
                        }
                        else if (item is RefeshReference)
                        {
                            RefreshData = (RefeshReference)item;
                        }
                    }
                }
                if (moduleData != null && serviceReqId != null && RefreshData != null)
                {
                    return new frmServiceReqSampleInfo(moduleData, serviceReqId, RefreshData);
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
