using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public enum STATUS
    {
        NEW = 1,
        IN_PROCESS = 2,
        FINISH = 3,
        WAIT_FOR_SUBCLINICAL = 4,
        WAIT_FOR_CONCLUSION = 5
    }

    public class KioskServiceSDO
    {
        public string ServiceName { get; set; }
        public string RoomName { get; set; }
        public string Address { get; set; }
        public STATUS Status { get; set; }
        public long? NumOrder { get; set; }
        public long? ResultingNumOrder { get; set; }
    }

    public class KioskInformationSDO
    {
        public string TreatmentCode { get; set; }
        public string Mobile { get; set; }
        public string HeinCardNumber { get; set; }
        public string PatientName { get; set; }
        public string GenderName { get; set; }
        public long Dob { get; set; }
        public bool HasNotDayDob { get; set; }
        public string HeinMediOrgName { get; set; }
        public string PatientAddress { get; set; }
        public List<KioskServiceSDO> Examinations { get; set; }
        public List<KioskServiceSDO> Subclinicals { get; set; }
    }
}
