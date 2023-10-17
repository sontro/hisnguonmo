using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TransDepartmentADO 
    {
        public long TreatmentId { get; set; }
        public long DepartmentId { get; set; }
       
        public TransDepartmentADO(long _treatmentId)
        {
            this.TreatmentId = _treatmentId;
        }
    }
}
