using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisTreatmentBedRoomSDO : HIS_TREATMENT_BED_ROOM
    {
        public long? TreatmentTypeId { get; set; }
        public long RequestRoomId { get; set; }
        public bool? IsAutoRemove { get; set; }
        public long? BedServiceTypeId { get; set; }
        public long? ShareCount { get; set; }
        public long? PatientTypeId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
    }
}
