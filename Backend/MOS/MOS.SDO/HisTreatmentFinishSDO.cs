using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisTreatmentFinishSDO
    {
        public long TreatmentId { get; set; }
        //id cua xu ly kham; khi ket thuc kham o man hinh xu ly kham
        public long? ServiceReqId { get; set; }
        public long? TreatmentResultId { get; set; }
        public long TreatmentEndTypeId { get; set; }
        public long? TreatmentEndTypeExtId { get; set; }
        public decimal TreatmentDayCount { get; set; }
        public long? MediRecordTypeId { get; set; }
        public long? TranPatiTechId { get; set; }
        public string HrmKskCode { get; set; }
        public long? OutPatientDateFrom { get; set; }
        public long? OutPatientDateTo { get; set; }
        public long? CareerId { get; set; }
        public string EndTypeExtNote { get; set; } // ghi chú kết thúc khám

        //Thong tin tai khoan 'Ky thay truong khoa', 'Ky thay giam doc vien'
        public string EndDeptSubsHeadLoginname { get; set; }
        public string EndDeptSubsHeadUsername { get; set; }
        public string HospSubsDirectorLoginname { get; set; }
        public string HospSubsDirectorUsername { get; set; }
        /// <summary>
        /// Tao benh an ngoai tru hay khong
        /// </summary>
        public bool CreateOutPatientMediRecord { get; set; }

        //Thong tin chuyen tuyen
        public long? TranPatiFormId { get; set; }
        public long? TranPatiReasonId { get; set; }
        public long? ProgramId { get; set; }
        public string ClinicalNote { get; set; }
        public string SubclinicalResult { get; set; }
        public string PatientCondition { get; set; }
        public string TransportVehicle { get; set; }
        public string Transporter { get; set; }
        public string TransferOutMediOrgCode { get; set; }
        public string TransferOutMediOrgName { get; set; }
        public string TranPatiHospitalLoginname { get; set; }
        public string TranPatiHospitalUsername { get; set; }

        //Thong tin tu vong
        public long? DeathTime { get; set; }
        public long? DeathCauseId { get; set; }
        public long? DeathWithinId { get; set; }
        public short? IsHasAupopsy { get; set; }
        public string MainCause { get; set; }
        public string Surgery { get; set; }
        public string DeathDocumentType { get; set; }
        public string DeathDocumentNumber { get; set; }
        public string DeathDocumentPlace { get; set; }
        public string DeathPlace { get; set; }
        public long? DeathDocumentDate { get; set; }
        public long? DeathCertBookId { get; set; }
        public long? DeathIssuedDate { get; set; }

        public long EndRoomId { get; set; }
        public long TreatmentFinishTime { get; set; }
        public string IcdText { get; set; }
        public string IcdName { get; set; }
        public string IcdCode { get; set; }
        public string TraditionalIcdName { get; set; }
        public string TraditionalIcdCode { get; set; }
        public string TraditionalIcdSubCode { get; set; }
        public string TraditionalIcdText { get; set; }

        public string IcdCauseName { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdSubCode { get; set; }
        public string DoctorLoginname { get; set; }
        public string DoctorUsernname { get; set; }
        public string EndCode { get; set; }
        public bool EndCodeRequest { get; set; }
        public bool OutCodeRequest { get; set; }
        public bool IsTemporary { get; set; }//phuc vu nguoi dung luu truoc thong tin ra vien chu chua ket thuc thuc su
        public bool? IsChronic { get; set; }// La man tinh

        //Thong tin hen kham
        public long? AppointmentTime { get; set; }
        public List<long> AppointmentExamRoomIds { get; set; }
        public long? AppointmentPeriodId { get; set; }
        public long? NumOrderBlockId { get; set; }

        //Thong tin nghi om/nghi duong thai
        public string SocialInsuranceNumber { get; set; }
        public decimal? SickLeaveDay { get; set; }
        public long? SickLeaveFrom { get; set; }
        public long? SickLeaveTo { get; set; }
        public string SickHeinCardNumber { get; set; }
        public string PatientRelativeType { get; set; }
        public string PatientRelativeName { get; set; }
        public string PatientWorkPlace { get; set; }
        public long? WorkPlaceId { get; set; }
        public List<HisBabySDO> Babies { get; set; }
        public string SickLoginname { get; set; }
        public string SickUsername { get; set; }
        public long? DocumentBookId { get; set; }

        //Thong tin hen mo
        public string AppointmentSurgery { get; set; }
        public long? SurgeryAppointmentTime { get; set; }

        //Thong tin ra vien
        public string Advise { get; set; }
        public string TreatmentMethod { get; set; }
        public string TreatmentDirection { get; set; }
        public string UsedMedicine { get; set; }
        public string ShowIcdCode { get; set; }
        public string ShowIcdName { get; set; }
        public string ShowIcdSubCode { get; set; }
        public string ShowIcdText { get; set; }
        public long? ExitDepartmentId { get; set; }

        //Co xuat XML4210 thong tuyen hay ko
        public bool IsExpXml4210Collinear { get; set; }

        //Co tao ho so dieu tri moi dua vao ho so dang ket thuc
        public bool IsCreateNewTreatment { get; set; }

        //Thong tin bo sung mat
        public string EyeTensionLeft { get; set; }
        public string EyeTensionRight { get; set; }
        public string EyesightLeft { get; set; }
        public string EyesightRight { get; set; }
        public string EyesightGlassLeft { get; set; }
        public string EyesightGlassRight { get; set; }

        public string ApproveFinishNote { get; set; }
        public bool IsApproveFinish { get; set; }
        public long? NewTreatmentInTime { get; set; }

        //Thong tin giay bao tu
        public long? DeathCertBookFirstId { get; set; }
        public long? DeathCertNumFirst { get; set; }
        public string DeathCertIssuerLoginname { get; set; }
        public string DeathCertIssuerUsername { get; set; }
        public short? DeathStatus { get; set; }
        public short? DeathDocumentTypeCode { get; set; }
        
        //Thong tin pha thai
        public bool? IsPregnancyTermination { get; set; }
        public long? GestationalAge { get; set; }
        public string PregnancyTerminationReason { get; set; }
        public long? PregnancyTerminationTime { get; set; }

        public HIS_PATIENT_TYPE_ALTER patientTypeAlter { get; set; }
    }
}
