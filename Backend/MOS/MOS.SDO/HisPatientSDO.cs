using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientSDO : HIS_PATIENT
    {
        public string HeinCardNumber { get; set; }
        public string HeinMediOrgCode { get; set; }
        public string HeinMediOrgName { get; set; }
        public long? HeinCardFromTime { get; set; }
        public long? HeinCardToTime { get; set; }
        public string HeinAddress { get; set; }
        public string Join5Year { get; set; }
        public string Paid6Month { get; set; }
        public string LiveAreaCode { get; set; }
        public string RightRouteCode { get; set; }
        public string RightRouteTypeCode { get; set; }
        public string HasBirthCertificate { get; set; }
        public long? KskContractId { get; set; }

        //Cac thong tin cua ho so dieu tri cua lan kham truoc do (tuong ung voi ma hen kham)
        public string TreatmentCode { get; set; }
        public string PatientProgramCode { get; set; }
        public long? TreatmentId { get; set; } //treatment_id tuong ung voi ma hen kham
        public long? ProgramId { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string IcdText { get; set; }
        public long? TreatmentTypeId { get; set; }
        public string AppointmentCode { get; set; }
        public long? AppointmentTime { get; set; }
        public long? NextExamNumOrder { get; set; }
        public long? NumOrderIssueId { get; set; }
        public string NextExamFromTime { get; set; }
        public string NextExamToTime { get; set; }
        public List<long> AppointmentExamRoomIds { get; set; }
        public long? AppointmentExamServiceId { get; set; }
        public long? InDate { get; set; }
        public short? IsPause { get; set; }
        
        //Thong tin chuyen tuyen
        public long? TransferInCmkt { get; set; }
        public string TransferInCode { get; set; }
        public long? TransferInFormId { get; set; }
        public string TransferInIcdCode { get; set; }
        public string TransferInIcdName { get; set; }
        public string TransferInMediOrgCode { get; set; }
        public string TransferInMediOrgName { get; set; }
        public long? TransferInReasonId { get; set; }
        public long? TransferInTimeFrom { get; set; }
        public long? TransferInTimeTo { get; set; }
        public string CardCode { get; set; }
        public long? CardId { get; set; }
        public List<HisPreviousPrescriptionSDO> PreviousPrescriptions { get; set; }
        public List<string> PreviousDebtTreatments { get; set; }
        public List<string> TodayFinishTreatments { get; set; }
        public List<PreviousDebtTreatmentSDO> PreviousDebtTreatmentDetails { get; set; }
        public V_HIS_TREATMENT_FEE_4 LastTreatmentFee { get; set; }
    }

    public class PreviousDebtTreatmentSDO
    {
        public string TDL_TREATMENT_CODE { get; set; }
        public long PATIENT_TYPE_ID { get; set; }
    }
}
