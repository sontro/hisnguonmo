using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MobaExamPresCreate.ADO
{
    public class TrackingADO : HIS_TRACKING
    {
        public string TrackingTimeStr { get; set; }
        public TrackingADO() { }
        public TrackingADO(HIS_TRACKING data)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<TrackingADO>(this, data);
                this.TrackingTimeStr = Inventec.Common.DateTime.Convert.TimeNumberToTimeStringWithoutSecond(data.TRACKING_TIME);

            }
        }
    }
}
