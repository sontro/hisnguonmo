using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisDepartmentTranSDO
    {
        public long Id { get; set; }
        public long TreatmentId { get; set; }
        public long DepartmentId { get; set; }
        public long Time { get; set; }
        public long RequestRoomId { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public bool IsReceive { get; set; }
        public bool AutoLeaveRoom { get; set; }
        public bool IsHospitalized { get; set; }
    }
}
