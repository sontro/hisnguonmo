using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExamRegisterDkkSDO
    {
        public string CardCode { get; set; }
        public string CardServiceCode { get; set; }
        public string PeopleCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Dob { get; set; }
        public long RequestRoomId { get; set; }
        public short? IsHasNotDayDob { get; set; }
        public string GenderCode { get; set; }
        public string BhytNumber { get; set; }
        public long? BhytFromTime { get; set; }
        public long? BhytToTime { get; set; }
        public string BhytAddress { get; set; }
        public string MediOrgCode { get; set; }
        public string MediOrgName { get; set; }
        public string LiveCode { get; set; }
        public string HospitalCode { get; set; }
        public string ServiceCode { get; set; }
        public string RoomCode { get; set; }
        public string PatientTypeCode { get; set; }
        public long RegisterDate { get; set; }
        public string RegisterReason { get; set; }
        public string RegisterSymptom { get; set; }
        public bool Join5Year { get; set; }
        public bool Paid6Month { get; set; }
        public string PeopleRegisterCode { get; set; }
        public long? PatientId { get; set; }

        public string Address { get; set; }
        public string CccdNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string EthnicCode { get; set; }
        public string EthnicName { get; set; }
        public string CommuneCode { get; set; }
        public string CommuneName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
    }
}
