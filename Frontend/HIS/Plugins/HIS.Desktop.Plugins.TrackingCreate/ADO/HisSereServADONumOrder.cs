using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TrackingCreate.ADO
{
    public class HisSereServADONumOrder : MOS.EFMODEL.DataModels.HIS_SERE_SERV
    {
        public long? NUM_ORDER { get; set; }
        public string TDL_SERVICE_UNIT_NAME { get; set; }
        public string RATION_TIME_NAME { get; set; }
        public long? USE_TIME { get; set; }
        public HisSereServADONumOrder()
        {
        }
        public HisSereServADONumOrder(HIS_SERE_SERV data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisSereServADONumOrder>(this, data);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

        public HisSereServADONumOrder(HIS_SERE_SERV data,HIS_SERVICE_REQ ser)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisSereServADONumOrder>(this, data);
                    this.USE_TIME = ser.USE_TIME;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
