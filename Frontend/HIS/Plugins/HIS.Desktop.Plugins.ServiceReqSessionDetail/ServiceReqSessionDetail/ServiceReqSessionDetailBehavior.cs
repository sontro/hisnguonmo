using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using HIS.Desktop.ADO;

namespace HIS.Desktop.Plugins.ServiceReqSessionDetail.ServiceReqSessionDetail
{
    class ServiceReqSessionDetailBehavior : Tool<IDesktopToolContext>, IServiceReqSessionDetail
    {
        object[] entity;

        internal ServiceReqSessionDetailBehavior()
            : base()
        {

        }

        internal ServiceReqSessionDetailBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IServiceReqSessionDetail.Run()
        {
            ServiceReqSessionDetailADO ServiceReqSessionDetail = null;
            Inventec.Desktop.Common.Modules.Module moduleData = null;
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is ServiceReqSessionDetailADO)
                            ServiceReqSessionDetail = ((ServiceReqSessionDetailADO)item);
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                    }
                }
                if (moduleData != null && ServiceReqSessionDetail != null)
                {
                    return new frmServiceReqSessionDetail(ServiceReqSessionDetail, moduleData);
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
