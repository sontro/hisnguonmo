using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisBabySDO
    {
        /// <summary>
        /// Trong truong hop update thi can gui ID
        /// </summary>
        public long? Id { get; set; }
        public string BabyName { get; set; }
        public long? GenderId { get; set; }
        public long? BornTypeId { get; set; }
        public long? BornResultId { get; set; }
        public long? BornPositionId { get; set; }
        public long? BornTime { get; set; }
        public long? BabyOrder { get; set; }
        public long? MonthCount { get; set; }
        public long? WeekCount { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Head { get; set; }
        public string Midwife { get; set; }
        public string FatherName { get; set; }
        public string EthnicCode { get; set; }
        public string EthnicName { get; set; }
        public long? BirthCertBookID { get; set; }
        public string IssuerLoginname { get; set; }
        public string IssuerUsername { get; set; }
        public long? CurrentAlive { get; set; }

        public long TreatmentId { get; set; }
        public BabyInfoForTreatmentSDO BabyInfoForTreatment { get; set; }
        /// <summary>
        /// Can cuoc cong dan hoac CMND
        /// </summary>
        public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngay cap
        /// </summary>
        public long? IssueDate { get; set; }
        /// <summary>
        /// Noi cap
        /// </summary>
        public string IssuePlace { get; set; }
        /// <summary>
        /// ngay cap giay chung sinh
        /// </summary>
        public long? IssueDateBaby { get; set; }

        public string MotherProvinceCode { get; set; }
        public string MotherProvinceName { get; set; }
        public string MotherDistrictCode { get; set; }
        public string MotherDistrictName { get; set; }
        public string MotherCommuneCode { get; set; }
        public string MotherCommuneName { get; set; }
        public string MotherAddress { get; set; }

        public string HtProvinceName { get; set; }
        public string HtDistrictName { get; set; }
        public string HtCommuneName { get; set; }
        public string HtAddress { get; set; }

        public long? NumberOfPregnancies { get; set; }
        public long? PostpartumCare { get; set; }
        public bool? IsDifficultBirth { get; set; }
        public bool? IsHaemorrhage { get; set; }
        public bool? IsUterineRupture { get; set; }
        public bool? IsPuerperal { get; set; }
        public bool? IsBacterialContamination { get; set; }
        public bool? IsTetanus { get; set; }
        public bool? IsMotherDeath { get; set; }
        public bool? IsFetalDeath22Weeks { get; set; }
        public bool? IsInjectK1 { get; set; }
        public bool? IsInjectB { get; set; }
        public string Birthplace { get; set; }
        public string MethodStyle { get; set; }
        public string Note { get; set; }

        public string HeinCardNumberTmp { get; set; }
        public long? DepartmentId { get; set; }
        public short? IsSurgery { get; set; }
        public long? NumberChildrenBirth { get; set; }
        public long? DeathDate { get; set; }

        public short? BirthplaceType { get; set; }
        public string BirthProvinceCode { get; set; }
        public string BirthProvinceName { get; set; }
        public string BirthDistrictCode { get; set; }
        public string BirthDistrictName { get; set; }
        public string BirthCommuneCode { get; set; }
        public string BirthCommuneName { get; set; }
        public string BirthHospitalCode { get; set; }
        public string BirthHospitalName { get; set; }
    }

    public class BabyInfoForTreatmentSDO
    {
        public long? NumberOfFullTermBirth { get; set; }
        public long? NumberOfPrematureBirth { get; set; }
        public long? NumberOfMiscarriage { get; set; }
        public short? NumberOfTests { get; set; }
        public short? TestHiv { get; set; }
        public short? TestSyphilis { get; set; }
        public short? TestHepatitisB { get; set; }
        public short? IsTestBloodSugar { get; set; }
        public short? IsEarlyNewbornCare { get; set; }
        public short? NewbornCareAtHome { get; set; }
        public long? NumberOfBirth { get; set; }
    }
}
