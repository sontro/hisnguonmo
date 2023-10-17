using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TrackingCreate.ADO
{
    class HisTrackingADO : HIS_TRACKING
    {
        public string DEPARTMENT_NAME { get; set; }
        public string TRACKING_TIME_STR { get; set; }

        public HisTrackingADO() { }

        public HisTrackingADO(HIS_TRACKING data) 
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisTrackingADO>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
