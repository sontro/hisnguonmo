using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCardPatientSDO : V_HIS_PATIENT
    {
        public V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public string CardCode { get; set; }
        public string ServiceCode { get; set; }
        public string PatientTypeName { get; set; }
        public string TreatmentTypeName { get; set; }
        public long PatientTypeId { get; set; }
        public long TreatmentTypeId { get; set; }
    }
}
