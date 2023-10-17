using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomWithIn.ADO
{
    class HisBedRoomV1 : MOS.EFMODEL.DataModels.V_HIS_BED_ROOM_1
    {
        public long TT_PATIENT_BED { get; set; }
        public string TT_PATIENT_BED_STR { get; set; }
        public long IsKey_ { get; set; }

        public long? BILL_PATIENT_TYPE_ID { get; set; }

        public bool? CHECK_IS_FULL { get; set; }
        public HisBedRoomV1() { }

        public HisBedRoomV1(MOS.EFMODEL.DataModels.V_HIS_BED_ROOM_1 data)
        {
            try
            {
                if (data != null)
                {
                    Inventec.Common.TypeConvert.Parse.ToUInt64(this.PATIENT_COUNT.ToString());
                    Inventec.Common.Mapper.DataObjectMapper.Map<HisBedRoomV1>(this, data);
                    this.TT_PATIENT_BED_STR = (int)this.PATIENT_COUNT + "/" + (int)this.BED_COUNT;
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}

