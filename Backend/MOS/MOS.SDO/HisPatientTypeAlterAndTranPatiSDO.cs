using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisPatientTypeAlterAndTranPatiSDO
    {
        public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public string TransferInMediOrgCode { get; set; }
        public string TransferInMediOrgName { get; set; }
        public long? TransferInFormId { get; set; }
        public long? TransferInReasonId { get; set; }
        public string TransferInIcdCode { get; set; }
        public string TransferInIcdName {get;set;}
        public string TransferInCode { get; set; }
        public long? TransferInCmkt { get; set; }
        public long? TransferInTimeFrom { get; set; }
        public long? TransferInTimeTo { get; set; }
        public byte[] ImgBhytData { get; set; }
    }
}
