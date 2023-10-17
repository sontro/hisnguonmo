using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentExtraEndInfoSDO
    {
        public long TreatmentId { get; set; }
        public long? TreatmentEndTypeExtId { get; set; }

        public string SocialInsuranceNumber { get; set; }
        //Thong tin hen mo
        public string AppointmentSurgery { get; set; }
        public long? SurgeryAppointmentTime { get; set; }
        public string Advise { get; set; }
        public string TreatmentMethod { get; set; }
        public string SickHeinCardNumber { get; set; }

        //Thong tin nghi om/nghi duong thai
        public decimal? SickLeaveDay { get; set; }
        public long? SickLeaveFrom { get; set; }
        public long? SickLeaveTo { get; set; }
        public string PatientRelativeType { get; set; }
        public string PatientRelativeName { get; set; }
        public string PatientWorkPlace { get; set; }
        public List<HisBabySDO> Babies { get; set; }
        public string SickLoginname { get; set; }
        public string SickUsername { get; set; }
        public long? DocumentBookId { get; set; }
        public long? WorkPlaceId { get; set; }
        //Thong tin pha thai
        public bool? IsPregnancyTermination { get; set; }
        public long? GestationalAge { get; set; }
        public string PregnancyTerminationReason { get; set; }
        public string EndTypeExtNote { get; set; }
        public long? PregnancyTerminationTime { get; set; }
    }
}
