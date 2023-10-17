using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisRegisterKioskSDO
    {
        public string PatientCode { get; set; }
        public string PatientTypeCode { get; set; }
        public string ServiceCode { get; set; }
        public string RoomCode { get; set; }
        public string RequestRoomCode { get; set; }
        public string HeinAddress { get; set; }
        public string FreeCoPaidTime { get; set; }
        public string HeinCardFromTime { get; set; }
        public string HeinCardNumber { get; set; }
        public string HeinCardToTime { get; set; }
        public string HeinOrgCode { get; set; }
        public string HeinOrgName { get; set; }
        public bool IsJoin5Year { get; set; }
        public string LiveAreaCode { get; set; }
        public bool IsPaid6Month { get; set; }
        public bool IsPriority { get; set; }
    }
}