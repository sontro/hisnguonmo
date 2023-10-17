using LIS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ConnectionTest.ADO
{
    public class LisMachineAdo : LIS_MACHINE
    {
        public long? MAX_SERVICE_PER_DAY { get; set; }
        public long? TOTAL_PROCESSED_SERVICE_TEIN { get; set; }
        public LisMachineAdo() { }


        public LisMachineAdo(LIS_MACHINE data, HisMachineCounterSDO sdo)
        {
            try
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<LIS_MACHINE>(this, data);
                if (sdo != null)
                {
                    this.MAX_SERVICE_PER_DAY = sdo.MAX_SERVICE_PER_DAY;
                    this.TOTAL_PROCESSED_SERVICE_TEIN = sdo.TOTAL_PROCESSED_SERVICE_TEIN;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}
