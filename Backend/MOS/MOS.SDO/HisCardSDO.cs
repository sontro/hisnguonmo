using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class AppointmentServiceSDO
    {
        public long ServiceId { get; set; }
        public decimal Amount { get; set; }
        public long? PatientTypeId { get; set; }
    }

    public class HisCardSDO
    {
        public string PatientCode { get; set; }
        public long? PatientId { get; set; }
        public string CardCode { get; set; }
        public string ServiceCode { get; set; }
        public string BankCardCode { get; set; }
        public string Address { get; set; }
        public long? CmndDate { get; set; }
        public string CmndNumber { get; set; }
        public string CmndPlace { get; set; }
        public long? CccdDate { get; set; }
        public string CccdNumber { get; set; }
        public string CccdPlace { get; set; }
        public string CommuneName { get; set; }
        public string DistrictName { get; set; }
        public long GenderId { get; set; }
        public string GenderCode { get; set; }
        public string GenderName { get; set; }
        public long Dob { get; set; }
        public string Email { get; set; }
        public string EthnicName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalName { get; set; }
        public string Phone { get; set; }
        public string ProvinceName { get; set; }
        public string ReligionName { get; set; }
        public string VirAddress { get; set; }
        public string HeinCardNumber { get; set; }
        public string HeinOrgCode { get; set; }
        public string HeinOrgName { get; set; }
        public string HeinAddress { get; set; }
        public string LevelCode { get; set; }
        public string RightRouteCode { get; set; }
        public string LiveAreaCode { get; set; }
        public string Join5Year { get; set; }
        public string Paid6Month { get; set; }
        public long? FreeCoPaidTime { get; set; }
        public long? Join5YearTime { get; set; }
        public long? HeinCardFromTime { get; set; }
        public long? HeinCardToTime { get; set; }
        public long? CareerId { get; set; }
        public string WorkPlace { get; set; }
        public short? IsHasNotDayDob { get; set; }
        public string AppointmentCode { get; set; }
        public string PreviousIcdCode { get; set; }
        public string PreviousIcdSubCode { get; set; }
        public string PreviousIcdName { get; set; }
        public string PreviousIcdText { get; set; }
        public long? PreviousAppointmentTime { get; set; }
        public long? PreviousTreatmentId { get; set; }
        public string HtCommuneName { get; set; }
        public string HtProvinceName { get; set; }
        public string HtDistrictName { get; set; }
        public string HtAddress { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        //public long? PrimaryPatientTypeId { get; set; }
        public List<AppointmentServiceSDO> AppointmentServices { get; set; }
    }
}
