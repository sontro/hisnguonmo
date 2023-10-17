using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class KidneyShiftScheduleADO
    {
        public delegate void DelegateProcessDataResult(object data);      

        public long TreatmentId { get; set; }
        public long IntructionTime { get; set; }
        public string PatientName { get; set; }
        public long PatientDob { get; set; }
        public string GenderName { get; set; }
        public DelegateProcessDataResult DgProcessDataResult { get; set; } 
               
    }
}
