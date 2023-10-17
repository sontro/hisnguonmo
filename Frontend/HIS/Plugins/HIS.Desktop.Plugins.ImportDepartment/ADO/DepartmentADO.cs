using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportDepartment.ADO
{
    public class DepartmentADO : MOS.EFMODEL.DataModels.HIS_DEPARTMENT
    {
        public string BRANCH_CODE { get; set; }
        public bool IsClinical { get; set; }
        public string IsClinicalStr { get; set; }
        public bool IsAutoReceivePatient { get; set; }
        public string IsAutoReceivePatientStr { get; set; }
        public string NumOrderStr { get; set; }
        public string TheoryPatientCountStr { get; set; }
        public string Error { get; set; }

        public string AllowTreatmentTypeCodes { get; set; }
        public string ALLOW_TREATMENT_TYPE_NAMEs { get; set; }
    }
}
