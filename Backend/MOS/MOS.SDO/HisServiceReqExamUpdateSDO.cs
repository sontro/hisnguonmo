using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqExamUpdateSDO
    {
        public long Id { get; set; }
        public long RequestRoomId { get; set; }

        //Dau hieu sinh ton
        public HIS_DHST HisDhst { get; set; }

        //Thong tin ICD
        public string IcdText { get; set; }
        public string IcdName { get; set; }
        public string IcdCode { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdCauseName { get; set; }
        public string IcdSubCode { get; set; }

        //Thong tin xu ly kham
        public long? SickDay { get; set; }
        public string HospitalizationReason { get; set; }

        public string PartExamDigestion { get; set; }
        public string PartExamDermatology { get; set; }
        public string FullExam { get; set; }
        public string PartExam { get; set; }
        public string PartExamCirculation { get; set; }
        public string PartExamEnt { get; set; }
        public string PartExamEye { get; set; }
        public string PartExamKidneyUrology { get; set; }
        public string PartExamMental { get; set; }
        public string PartExamNutrition { get; set; }
        public string PartExamMotion { get; set; }
        public string PartExamObstetric { get; set; }
        public string PartExamMuscleBone { get; set; }
        public string PartExamNeurological { get; set; }
        public string PartExamOend { get; set; }
        public string PartExamRespiratory { get; set; }
        public string PartExamStomatology { get; set; }
        public string PathologicalHistory { get; set; }
        public string PathologicalHistoryFamily { get; set; }
        public string PathologicalProcess { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public long? FinishTime { get; set; }
        public string Conclusion { get; set; }
        public string Advise { get; set; }

        //Thong tin xu ly kham mat
        public string PartExamEyeTensionRight { get; set; }
        public string PartExamEyeTensionLeft { get; set; }
        public string PartExamEyeSightRight { get; set; }
        public string PartExamEyeSightLeft { get; set; }
        public string PartExamEyeSightGlassRight { get; set; }
        public string PartExamEyeSightGlassLeft { get; set; }
        public long? PartExamHorizontalSight { get; set; }
        public long? PartExamVerticalSight { get; set; }
        public long? PartExamEyeBlindColor { get; set; }

        //Xu ly kham tai mui hong
        public string PartExamEar { get; set; }
        public string PartExamNose { get; set; }
        public string PartExamThroat { get; set; }

        //Kham tai
        public string PartExamEarRightNormal { get; set; }
        public string PartExamEarRightWhisper { get; set; }
        public string PartExamEarLeftNormal { get; set; }
        public string PartExamEarLeftWhisper { get; set; }

        //Kham hong
        public string PartExamUpperJaw { get; set; }
        public string PartExamLowerJaw { get; set; }

        public string NextTreaIntrCode { get; set; }
        public string NextTreaIntrName { get; set; }

        public string Note { get; set; }
        public string Subclinical { get; set; }
        public string TreatmentInstruction { get; set; }
        public bool IsFinish { get; set; } //Xu tri co thuc hien ket thuc y/c kham hay khong

        //Kham suc khoe (tich hop pm nhan su)
        public long? HealthExamRankId { get; set; }

        //Truong ho benh
        public long? PatientCaseId { get; set; }

        //Thong tin Chong chi dinh
        public List<long> ContraindicationIds { get; set; }


        //Thong tin hen kham lai luc xu tri kham them hoac xu tri ket thuc kham
        public long? AppointmentTime { get; set; }
        public string AppointmentDesc { get; set; }

        public long? AppointmentExamRoomId { get; set; } //id phong kham duoc nhap luc ket thuc hen kham
        public long? AppointmentExamServiceId { get; set; } //Dich vu kham khi "hen kham"

        public string NotePatient { get; set; }

        //Thong tin ket thuc dieu tri, trong truong hop xu ly kem ket thuc dieu tri
        public HisTreatmentFinishSDO TreatmentFinishSDO { get; set; }
        //Thong tin kham them
        public HisServiceReqExamAdditionSDO ExamAdditionSDO { get; set; }
        //Thong tin nhap vien
        public HisDepartmentTranHospitalizeSDO HospitalizeSDO { get; set; }
    }
}
