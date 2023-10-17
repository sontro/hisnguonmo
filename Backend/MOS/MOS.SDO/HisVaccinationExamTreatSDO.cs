using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    /// <summary>
    /// Ket luan khi kham sang loc tiem chung
    /// </summary>
    public enum VaccinationExamConcludeEnum
    {
        /// <summary>
        /// Du dieu kien tiem
        /// </summary>
        OK = 1,
        /// <summary>
        /// Khong du dieu kien tiem
        /// </summary>
        NOK = 2
    }

    public class HisVaccinationExamTreatSDO
    {
        public long VaccinationExamId { get; set; }
        public long WorkingRoomId { get; set; }
        public string Note { get; set; }
        public string PtAllergicHistory { get; set; }
        public string PtPathologicalHistory { get; set; }
        public string ExecuteLoginname { get; set; }
        public string ExecuteUsername { get; set; }
        public List<HisVaexVaerSDO> VaexVaerInfos { get; set; }
        public short? VaccinationExamSttId { get; set; }
        public short? IsTestHBSAG { get; set; }
        public short? IsPositiveResult { get; set; }
        public short? IsSpecialistExam { get; set; }
        public long? SpecialistDepartmentId { get; set; }
        public string SpecialistReason { get; set; }
        public string SpecialistResult { get; set; }
        public string SpecialistConclude { get; set; }
        public long? RabiesNumberOfDays { get; set; }
        public bool? IsRabiesAnimalDog { get; set; }
        public bool? IsRabiesAnimalCat { get; set; }
        public bool? IsRabiesAnimalBat { get; set; }
        public bool? IsRabiesAnimalOther { get; set; }
        public bool? IsRabiesWoundLocationHead { get; set; }
        public bool? IsRabiesWoundLocationFace { get; set; }
        public bool? IsRabiesWoundLocationNeck { get; set; }
        public bool? IsRabiesWoundLocationHand { get; set; }
        public bool? IsRabiesWoundLocationFoot { get; set; }
        public short? RabiesWoundRank { get; set; }
        public short? RabiesWoundStatus { get; set; }
        public VaccinationExamConcludeEnum? Conclude { get; set; }
        public HIS_DHST Dhst { get; set; }
    }
}
