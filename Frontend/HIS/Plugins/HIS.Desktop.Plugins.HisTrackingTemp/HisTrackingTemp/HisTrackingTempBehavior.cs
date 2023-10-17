using Inventec.Core;
using Inventec.Desktop.Common;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.HisTrackingTemp.HisTrackingTemp
{
    class HisTrackingTempBehavior : BusinessBase, IHisTrackingTemp
    {
        object[] entity;
        internal HisTrackingTempBehavior(CommonParam param, object[] filter)
            : base()
        {
            this.entity = filter;
        }

        object IHisTrackingTemp.Run()
        {
            try
            {
                Inventec.Desktop.Common.Modules.Module moduleData = null;
                HIS_TRACKING_TEMP HIS_TRACKING_TEMP = null;
                TrackingTempADO trackingTempADO = null;
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
                            else if (entity[i] is HIS_TRACKING_TEMP)
                            {
                                HIS_TRACKING_TEMP = (HIS_TRACKING_TEMP)entity[i];
                            }
                            else if (entity[i] is TrackingTempADO)
                            {
                                trackingTempADO = (TrackingTempADO)entity[i];
                            }

                        }
                    }
                }
                if (trackingTempADO != null)
                {
                    Inventec.Common.Logging.LogSystem.Info("trackingTempADO");
                    return new frmHisTrackingTemp(moduleData, trackingTempADO);
                }
                else
                {
                    return new frmHisTrackingTemp(moduleData, HIS_TRACKING_TEMP);
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
