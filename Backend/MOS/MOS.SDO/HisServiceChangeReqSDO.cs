using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceChangeReqSDO
    {
        public long SereServId { get; set; }
        public long AlterServiceId { get; set; }
        public long PatientTypeId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
        public long WorkingRoomId { get; set; }
    }
}
