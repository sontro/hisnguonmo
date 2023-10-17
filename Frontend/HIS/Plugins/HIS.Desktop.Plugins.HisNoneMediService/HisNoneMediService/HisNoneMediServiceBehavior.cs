using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisNoneMediService
{
    class HisNoneMediServiceBehavior : BusinessBase, IHisNoneMediService
    {
        object[] entity;
        RefeshReference delegateRefresh;
        internal HisNoneMediServiceBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisNoneMediService.Run()
        {
            object result = null;
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                

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
                            else if (entity[i] is RefeshReference)
                            {
                                delegateRefresh = (RefeshReference)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && delegateRefresh != null)
                {
                    result = new frmHisNoneMediService(moduleData, delegateRefresh);
                }
                else
                {
                    result = new frmHisNoneMediService(moduleData);
                }
                return result;


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
