using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisDepartmentTranHospitalizeSDO
    {
        public long TreatmentId { get; set; }
        public long DepartmentId { get; set; }
        public long Time { get; set; }
        public long TreatmentTypeId { get; set; }
        public long? BedRoomId { get; set; }
        public long RequestRoomId { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string TraditionalIcdCode { get; set; }
        public string TraditionalIcdName { get; set; }
        public string TraditionalIcdSubCode { get; set; }
        public string TraditionalIcdText { get; set; }
        public string IcdText { get; set; }
        public string IcdSubCode { get; set; }
        public bool IsEmergency { get; set; }
        public string RelativeName { get; set; }
        public string RelativePhone { get; set; }
        public string RelativeAddress { get; set; }
        public long? CareerId { get; set; }
        public string InHospitalizationReasonCode { get; set; } //Mã lý do nhập viện
        public string InHospitalizationReasonName { get; set; } //lý do nhập viện
    }
}
