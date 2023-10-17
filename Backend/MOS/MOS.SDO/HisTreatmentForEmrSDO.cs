using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentForEmrSDO
    {
        public long TreatmentId { get; set; }
        public long PatientId { get; set; }
        public long IntructionTime { get; set; }
        public long? WorkingDepartmentId { get; set; }

        public V_HIS_TREATMENT Treatment { get; set; }
        public V_HIS_PATIENT Patient { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public V_HIS_TREATMENT_BED_ROOM TreatmentBedRoom { get; set; }
        public HIS_DHST Dhst { get; set; }
        public V_HIS_BABY Baby { get; set; }
        public List<V_HIS_DEPARTMENT_TRAN> DepartmentTrans { get; set; }
        public List<V_HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<V_HIS_SERE_SERV_PTTT> SereServPttts { get; set; }
        public List<HIS_SERE_SERV> SereServs { get; set; }
        public HIS_DHST RecentDhst { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> ExpMestMedicines { get; set; }
        public long TreatmentIcdCount { get; set; }

    }
}
