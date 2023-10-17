using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisServiceReqKskExecuteSDO
    {
        public long ServiceReqId { get; set; }
        public bool isFinish { get; set; }

        public long RequestRoomId { get; set; }

        public string ConclusionClinical { get; set; }
        public string ConclusionSubclinical { get; set; }
        public string OccupationalDisease { get; set; }
        public string ConclusionConsultation { get; set; }
        public string ProvisionalDiagnosis { get; set; }
        public string ExamConclusion { get; set; }
        public string Conclusion { get; set; }

        public HisKskGeneralSDO KskGeneral { get; set; }
        public HisKskOccupationalSDO KskOccupational { get; set; }
    }

    public class HisKskGeneralSDO
    {
        public HIS_DHST HisDhst { get; set; }

        public string ExamCirculation { get; set; }
        public long? ExamCirculationRank { get; set; }
        public string ExamDermatology { get; set; }
        public long? ExamDermatologyRank { get; set; }
        public string ExamDigestion { get; set; }
        public long? ExamDigestionRank { get; set; }
        public string ExamEnt { get; set; }
        public long? ExamEntRank { get; set; }
        public string ExamEye { get; set; }
        public long? ExamEyeRank { get; set; }
        public string ExamKidneyUrology { get; set; }
        public long? ExamKidneyUrologyRank { get; set; }
        public string ExamMental { get; set; }
        public long? ExamMentalRank { get; set; }
        public string ExamMuscleBone { get; set; }
        public long? ExamMuscleBoneRank { get; set; }
        public string ExamNeurological { get; set; }
        public long? ExamNeurologicalRank { get; set; }
        public string ExamOend { get; set; }
        public long? ExamOendRank { get; set; }
        public string ExamRespiratory { get; set; }
        public long? ExamRespiratoryRank { get; set; }
        public string ExamStomatology { get; set; }
        public long? ExamStomatologyRank { get; set; }
        public string ExamSurgery { get; set; }
        public long? ExamSurgeryRank { get; set; }

        public string ExamObstetric { get; set; }
        public long? ExamObstetricRank { get; set; }
        public string ExamOccupationalTherapy { get; set; }
        public long? ExamOccupationalTherapyRank { get; set; }
        public string ExamTraditional { get; set; }
        public long? ExamTraditionalRank { get; set; }
        public string ExamNutrion { get; set; }
        public long? ExamNutrionRank { get; set; }

        public string NoteBiochemical { get; set; }
        public string NoteBlood { get; set; }
        public string NoteProstase { get; set; }
        public string NoteSupersonic { get; set; }
        public string NoteXray { get; set; }

        public long? HealthExamRankId { get; set; }
        public string Diseases { get; set; }
        public string TreatmentInstruction { get; set; }

        public string NoteDiim { get; set; }
        public string NoteTestUrine { get; set; }
        public string NoteTestOther { get; set; }

        public long? ConclusionTime { get; set; }
        public string ConcluderUserName { get; set; }
        public string ConcluderLoginName { get; set; }
        public string HeinMediOrgCode { get; set; }
    }

    public class HisKskOccupationalSDO
    {
        public HIS_DHST HisDhst { get; set; }

        public string ExamCapillary { get; set; }
        public long? ExamCapillaryRank { get; set; }
        public string ExamCardiovascular { get; set; }
        public long? ExamCardiovascularRank { get; set; }
        public string ExamCirculation { get; set; }
        public long? ExamCirculationRank { get; set; }
        public string ExamDermatology { get; set; }
        public long? ExamDermatologyRank { get; set; }
        public string ExamDigestion { get; set; }
        public long? ExamDigestionRank { get; set; }
        public string ExamEnt { get; set; }
        public long? ExamEntRank { get; set; }
        public string ExamEye { get; set; }
        public long? ExamEyeRank { get; set; }
        public string ExamHematopoietic { get; set; }
        public long? ExamHematopoieticRank { get; set; }
        public string ExamKidneyUrology { get; set; }
        public long? ExamKidneyUrologyRank { get; set; }
        public string ExamLymphNodes { get; set; }
        public long? ExamLymphNodesRank { get; set; }
        public string ExamMental { get; set; }
        public long? ExamMentalRank { get; set; }
        public string ExamMotion { get; set; }
        public long? ExamMotionRank { get; set; }
        public string ExamMucosa { get; set; }
        public long? ExamMucosaRank { get; set; }
        public string ExamMuscleBone { get; set; }
        public long? ExamMuscleBoneRank { get; set; }
        public string ExamNail { get; set; }
        public long? ExamNailRank { get; set; }
        public string ExamNeurological { get; set; }
        public long? ExamNeurologicalRank { get; set; }
        public string ExamOend { get; set; }
        public long? ExamOendRank { get; set; }
        public string ExamRespiratory { get; set; }
        public long? ExamRespiratoryRank { get; set; }
        public string ExamStomatology { get; set; }
        public long? ExamStomatologyRank { get; set; }
        public string ExamSurgery { get; set; }
        public long? ExamSurgeryRank { get; set; }

        public string Conclusion { get; set; }
        public string Diseases { get; set; }
        public long? HealthExamRankId { get; set; }
        public string TreatmentInstruction { get; set; }
    }

    public class KskExecuteResultSDO
    {
        public V_HIS_SERVICE_REQ HisServiceReq { get; set; }
        public HIS_KSK_GENERAL HisKskGeneral { get; set; }
        public HIS_KSK_OCCUPATIONAL HisKskOccupational { get; set; }
    }
}
