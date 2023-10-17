using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class EpidemiologyInfoSDO
    {
        public long TreatmentId { get; set; }
        public long? VaccineId { get; set; }
        public long? VaccinationOrder { get; set; }
        public long? EpidemiologyContactType { get; set; }
        public string EpidemiologySympton { get; set; }
        public string CovidPatientCode { get; set; }
    }
}
