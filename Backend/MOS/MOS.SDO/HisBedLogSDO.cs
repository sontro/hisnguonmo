using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisBedLogSDO
    {
        public long? Id { get; set; }
        public long TreatmentBedRoomId { get; set; }
        public long BedId { get; set; }
        public long StartTime { get; set; }
        public long? FinishTime { get; set; }
        public long? BedServiceTypeId { get; set; }
        public long? ShareCount { get; set; }
        public long? PatientTypeId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
        public long WorkingRoomId { get; set; }
    }
}
