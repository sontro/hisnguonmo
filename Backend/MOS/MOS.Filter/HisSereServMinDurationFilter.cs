using System.Collections.Generic;

namespace MOS.Filter
{
    public class ServiceDuration
    {
        public long ServiceId { get; set; }
        public long MinDuration { get; set; } //khoang thoi gian tinh bang "ngày"
    }

    public class HisSereServMinDurationFilter
    {
        public long ServiceReqId { get; set; } //service_req_id trong truong hop check khi update
        public long InstructionTime { get; set; }
        public long PatientId { get; set; }
        public List<ServiceDuration> ServiceDurations { get; set; }
    }
}
