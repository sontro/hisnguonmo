using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class PatientTypeDepartmentADO
    {
        public V_HIS_DEPARTMENT_TRAN departmentTran { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER patientTypeAlter { get; set; }
        public V_HIS_CO_TREATMENT coTreatment { get; set; }
        public object HisPatyAlterDetail { get; set; }
        public long type { get; set; }
        public short? IS_ACTIVE { get; set; }
        public long? CREATE_TIME { get; set; }
        public long? MODIFY_TIME { get; set; }
        public long LOG_TIME { get; set; }
        public long TREATMENT_ID { get; set; }
        public long PATIENT_ID { get; set; }
        public string CREATOR { get; set; }
        public string MODIFIER { get; set; }
        public string Id { get; set; }
        public string ParentId { get; set; }
        public long? HEIN_CARD_FROM_TIME
        {
            get;
            set;
        }

        public long? HEIN_CARD_TO_TIME
        {
            get;
            set;
        }

    }
}
