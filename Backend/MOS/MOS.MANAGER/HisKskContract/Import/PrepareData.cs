using MOS.EFMODEL.DataModels;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisKskContract.Import
{
    class PrepareData
    {
        public HIS_KSK_CONTRACT KskContract { get; set; }
        public HIS_PATIENT Patient { get; set; }
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public List<HIS_KSK_SERVICE> KskServices { get; set; }
        public HisKskPatientSDO HisKskPatientSDO { get; set; }
    }
}
