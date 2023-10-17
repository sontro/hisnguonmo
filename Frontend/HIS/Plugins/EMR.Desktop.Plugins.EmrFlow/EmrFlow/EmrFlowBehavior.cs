using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;
using EMR.EFMODEL.DataModels;
using EMR.Desktop.Plugins.EmrFlow.EmrFlow;

namespace EMR.Desktop.Plugins.EmrFlow.EmrFlow
{
    class EmrFlowBehavior: BusinessBase,IEmrFlow
    {
         object[] entity;
         internal EmrFlowBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

         object IEmrFlow.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                EMR.EFMODEL.DataModels.V_EMR_FLOW EmrFlow = null;
                EMR_BUSINESS EmrBusiness = null ;
                     
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
                            if (entity[i] is EMR_FLOW)
                            {
                                EmrFlow = (V_EMR_FLOW)entity[i];
                            }
                            if (entity[i] is EMR_BUSINESS)
                            {
                                EmrBusiness = (EMR_BUSINESS)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && EmrFlow != null)
                {
                    return new frmEmrFlow(moduleData, EmrFlow);
                }
                else if (moduleData != null && EmrBusiness != null)
                {
                    return new frmEmrFlow(moduleData, EmrBusiness);
                }
                else
                {
                    return new frmEmrFlow(moduleData);
                }
                
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
