using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisDepartmentTranReceiveSDO
    {
        public long InTime { get; set; }
        public long DepartmentId { get; set; }
        public long DepartmentTranId { get; set; }
        public long? BedRoomId { get; set; }
        public long? BedId { get; set; }
        public long? BedServiceId { get; set; }
        public long? TreatmentTypeId { get; set; }
        public long RequestRoomId { get; set; }
        public string InCode { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string TraditionalIcdCode { get; set; }
        public string TraditionalIcdName { get; set; }
        public string TraditionalIcdSubCode { get; set; }
        public string TraditionalIcdText { get; set; }
        public string IcdText { get; set; }
        public string IcdSubCode { get; set; }
        public bool ReIssueInCode { get; set; }

        public long? PatientTypeId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
        public long? ShareCount { get; set; }
        public long? PatientClassifyId { get; set; }
    }
}
