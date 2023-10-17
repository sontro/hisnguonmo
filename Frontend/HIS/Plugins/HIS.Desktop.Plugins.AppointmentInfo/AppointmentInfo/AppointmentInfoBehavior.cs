using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.AppointmentInfo.AppointmentInfo
{
    class AppointmentInfoBehavior : BusinessBase, IAppointmentInfo
    {
        object[] entity;
        internal AppointmentInfoBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IAppointmentInfo.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                V_HIS_TREATMENT_4 treatment = null;
                RefeshReference refresh = null;

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
                            else if (entity[i] is V_HIS_TREATMENT_4)
                            {
                                treatment = (V_HIS_TREATMENT_4)entity[i];
                            }
                            else if (entity[i] is RefeshReference)
                            {
                                refresh = (RefeshReference)entity[i];
                            }
                        }
                    }
                }

                if (treatment != null)
                {
                    return new fromAppointmentInfo(moduleData, treatment, refresh);
                }
                else
                {
                    return null;
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
