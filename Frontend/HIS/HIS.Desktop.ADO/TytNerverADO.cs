using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TytNerverADO
    {
        public string branchCode{ get; set; }
        public string patientCode{ get; set; }

        public TytNerverADO(string _branchCode, string _patientCode)
        {
            this.branchCode = _branchCode;
            this.patientCode = _patientCode;
        }
    }
}
