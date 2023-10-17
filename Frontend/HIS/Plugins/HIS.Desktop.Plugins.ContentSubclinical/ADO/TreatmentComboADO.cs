using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ContentSubclinical.ADO
{
    public class TreatmentComboADO
    {
        public long TreatmentId { get; set; }
        public string TreatmentCode { get; set; }
        public string InTime { get; set; }
        public string EndDepartmentName { get; set; }
        public TreatmentComboADO()
        {

        }
        public TreatmentComboADO(long treatmentId, string treatmentCode, string inTime, string endDepartmentName)
        {
            TreatmentId = treatmentId;
            TreatmentCode = treatmentCode;
            InTime = inTime;
            EndDepartmentName = endDepartmentName;
        }
    }
}
