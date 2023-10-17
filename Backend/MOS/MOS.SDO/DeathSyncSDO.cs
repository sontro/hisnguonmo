using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class DeathSyncSDO
    {
        public HIS_PATIENT PatientData { get; set; }
        public V_HIS_TREATMENT TreatmentData { get; set; }
        public HIS_SEVERE_ILLNESS_INFO DeathData { get; set; }
        public string FileBase64Str { get; set; }
    }
}
