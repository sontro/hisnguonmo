using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class TrackingServiceReq
    {
        public long ServiceReqId { get; set; }
        public bool IsNotShowMaterial { get; set; }
        public bool IsNotShowMedicine { get; set; }
        public bool IsNotShowOutMate { get; set; }
        public bool IsNotShowOutMedi { get; set; }
    }

    public class HisTrackingSDO
    {
        public HIS_TRACKING Tracking { get; set; }
        public HIS_DHST Dhst { get; set; }
        public List<TrackingServiceReq> ServiceReqs { get; set; }
        public List<long> UsedForServiceReqIds { get; set; }
        public long WorkingRoomId { get; set; }
        public List<long> CareIds { get; set; }
        public List<long> DebateIds { get; set; }
    }
}
