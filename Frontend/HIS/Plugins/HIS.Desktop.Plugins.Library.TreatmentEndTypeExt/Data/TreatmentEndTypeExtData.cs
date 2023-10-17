using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt.Data
{
    public class TreatmentEndTypeExtData
    {
        public long? TreatmentEndTypeExtId { get; set; }
        public decimal? SickLeaveDay { get; set; }
        public long? SickLeaveFrom { get; set; }
        public long? SickLeaveTo { get; set; }
        public long? WorkPlaceId { get; set; }
        public string Loginname { get; set; }
        public string Username { get; set; }
        public string PatientWorkPlace { get; set; }
        public string PatientRelativeName { get; set; }
        public string PatientRelativeType { get; set; }
        public string SickHeinCardNumber { get; set; }
        public List<BabyADO> Babes { get; set; }
        public long? DocumentBookId { get; set; }

        public long? EndTime { get; set; }

        //SurgeryAppointment
        public long? SurgeryAppointmentTime { get; set; }
        public string Advise { get; set; }
        public string AppointmentSurgery { get; set; }
        public string SocialInsuranceNumber { get; set; }
        public string EndTypeExtNote { get; set; }
        public string ExtraEndCode { get; set; }
        public bool? IsPregnancyTermination
        {
            get;
            set;
        }

        public long? GestationalAge
        {
            get;
            set;
        }

        public string PregnancyTerminationReason
        {
            get;
            set;
        }
        public string TreatmentMethod
        {
            get;
            set;
        }
        public long? PregnancyTerminationTime
        {
            get;
            set;
        }

    }
}
