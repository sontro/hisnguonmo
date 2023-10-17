using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqUpdateInstruction.ServiceReqUpdateInstruction
{
    class ServiceReqUpdateInstructionBehavior : BusinessBase, IServiceReqUpdateInstruction
    {
        long service_req_id;
        object[] entity;
        internal ServiceReqUpdateInstructionBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }
        object IServiceReqUpdateInstruction.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                RefeshReference refreshData = null;
                if (entity.GetType() == typeof(object[]))
                {
                    if (entity != null && entity.Count() > 0)
                    {

                        for (int i = 0; i < entity.Count(); i++)
                        {
                            if (entity[i] is Inventec.Desktop.Common.Modules.Module)
                            {
                                moduleData = (Inventec.Desktop.Common.Modules.Module)entity[i];
                            }
                            if (entity[i] is RefeshReference)
                            {
                                refreshData = (RefeshReference)entity[i];
                            }
                            if (entity[i] is long)
                            {
                                service_req_id = (long)entity[i];
                            }
                        }
                    }
                }
                return new frmServiceReqUpdateInstruction(moduleData, service_req_id, refreshData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
