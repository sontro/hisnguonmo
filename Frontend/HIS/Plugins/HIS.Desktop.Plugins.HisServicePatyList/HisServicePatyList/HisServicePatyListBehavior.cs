using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisServicePatyList.HisServicePatyList
{
    class HisServicePatyListBehavior : BusinessBase, IHisServicePatyList
    {
        object[] entity;
        internal HisServicePatyListBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisServicePatyList.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY servicePaty = null;
                V_HIS_SERVICE service = null ;
                     
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
                            if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY)
                            {
                                servicePaty = (MOS.EFMODEL.DataModels.V_HIS_SERVICE_PATY)entity[i];
                            }
                            if (entity[i] is MOS.EFMODEL.DataModels.V_HIS_SERVICE)
                            {
                                service = (MOS.EFMODEL.DataModels.V_HIS_SERVICE)entity[i];
                            }
                        }
                    }
                }
                if (moduleData != null && servicePaty != null)
                {
                    return new frmHisServicePatyList(moduleData, servicePaty);
                }
                else if (moduleData != null && service != null)
                {
                    return new frmHisServicePatyList(moduleData, service);
                }
                else
                {
                    return new frmHisServicePatyList(moduleData);
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
