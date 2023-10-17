using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignPaan.ADO
{
    public class TrackingAdo : MOS.EFMODEL.DataModels.V_HIS_TRACKING
    {
        public string TrackingTimeStr { get; set; }

        public TrackingAdo()
        {
            TrackingTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.TRACKING_TIME);
        }

        public TrackingAdo(MOS.EFMODEL.DataModels.V_HIS_TRACKING _data)
        {
            try
            {
                if (_data != null)
                {
                    System.Reflection.PropertyInfo[] pi = Inventec.Common.Repository.Properties.Get<MOS.EFMODEL.DataModels.V_HIS_TRACKING>();

                    foreach (var item in pi)
                    {
                        item.SetValue(this, (item.GetValue(_data)));
                    }
                    TrackingTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeString(this.TRACKING_TIME);
                }
            }

            catch (Exception)
            {

            }
        }
    }
}
