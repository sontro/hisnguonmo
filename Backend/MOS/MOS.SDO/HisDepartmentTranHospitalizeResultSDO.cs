using MOS.EFMODEL.DataModels;

namespace MOS.SDO
{
    public class HisDepartmentTranHospitalizeResultSDO
    {
        public V_HIS_DEPARTMENT_TRAN DepartmentTran { get; set; }
        public V_HIS_PATIENT_TYPE_ALTER PatientTypeAlter { get; set; }
        public HIS_TREATMENT Treatment { get; set; }
        public V_HIS_PATIENT Patient { get; set; }
    }
}
