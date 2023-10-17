using HIS.Desktop.ADO;
using HIS.Desktop.Common;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AntibioticRequest.AntibioticRequest
{
    class AntibioticRequestBehavior : Tool<IDesktopToolContext>, IAntibioticRequest
    {
        object[] entity;
        Inventec.Desktop.Common.Modules.Module moduleData = null;
        AntibioticRequestADO antibioticRequest = null;
        RefeshReference delegateRefresh = null;
        internal AntibioticRequestBehavior()
            : base()
        {

        }

        internal AntibioticRequestBehavior(CommonParam param, object[] data)
            : base()
        {
            entity = data;
        }

        object IAntibioticRequest.Run()
        {
            try
            {
                if (entity != null && entity.Count() > 0)
                {
                    foreach (var item in entity)
                    {
                        if (item is Inventec.Desktop.Common.Modules.Module)
                            moduleData = (Inventec.Desktop.Common.Modules.Module)item;
                        else if(item is AntibioticRequestADO)
                            antibioticRequest= (AntibioticRequestADO)item;
                        else if (item is RefeshReference)                
                            delegateRefresh = (RefeshReference)item;                       
                    }
                }

                if (moduleData != null)
                {
                    return new HIS.Desktop.Plugins.AntibioticRequest.Run.frmAntibioticRequest(moduleData, antibioticRequest, delegateRefresh);
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
