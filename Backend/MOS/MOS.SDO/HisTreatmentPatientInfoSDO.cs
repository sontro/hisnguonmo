using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisPatientInfoSDO
    {
        public long? TreatmentId { get; set; }
        public long? CareerId { get; set; }
        public long Dob { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public long GenderId { get; set; }
        public short? IsHasNotDayDob { get; set; }
        public long? WorkPlaceId { get; set; }
        public long? MilitaryRankId { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string CommuneCode { get; set; }
        public string CommuneName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public string RelativeType { get; set; }
        public string RelativeName { get; set; }
        public string RelativeAddress { get; set; }
        public string Email { get; set; }
        public string EthnicCode { get; set; }
        public string WorkPlace { get; set; }
        public string EthnicName { get; set; }
        public string ReligionName { get; set; }
        public string RelativeCmndNumber { get; set; }
        public string PathologicalHistory { get; set; }
        public string PathologicalHistoryFamily { get; set; }
        public string PatientStoreCode { get; set; }
        public byte[] ImgBhytData { get; set; }
        public byte[] ImgAvatarData { get; set; }
        public string NationalCode { get; set; }
        public string NationalName { get; set; }
        public string TaxCode { get; set; }

        public string CmndNumber { get; set; }
        public long? CmndDate { get; set; }
        public string CmndPlace { get; set; }

        public string CccdNumber { get; set; }
        public long? CccdDate { get; set; }
        public string CccdPlace { get; set; }

        public string AccountNumber { get; set; }
        public string BloodAboCode { get; set; }
        public string BloodRhCode { get; set; }
        public string HtAddres { get; set; }
        public string HtProvinceName { get; set; }
        public string HtDistrictName { get; set; }
        public string HtCommuneName { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string RelativeMobile { get; set; }
        public string RelativePhone { get; set; }
        public string Uuid { get; set; }
        public string UuidBhytNumber { get; set; }

        public string MotherCareer {get;set;}
        public string MotherEducationalLevel {get;set;}
        public string FatherCareer { get; set; }
        public string FatherEducationalLevel { get; set; }

        public long? PatientClassifyId { get; set; }
        public bool? IsUpdateEmr { get; set; }
        public short? IsChronic { get; set; }
    }
}
