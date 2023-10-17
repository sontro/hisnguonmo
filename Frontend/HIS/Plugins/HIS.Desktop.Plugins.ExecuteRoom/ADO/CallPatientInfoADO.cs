using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoom.ADO
{
    class CallPatientInfoADO
    {
        public string PatientName { get; set; }
        public string Dob { get; set; }
        public long NumOrder { get; set; }
        public long PatientType { get; set; }
        public long ServiceReqId { get; set; }
        public CallPatientInfoADO() { }
        public CallPatientInfoADO(string PatientName_, string Dob_, long NumOrder_, long PatientType_, long ServiceReqId_)
        {
            this.PatientName = PatientName_;
            this.Dob = Dob_;
            this.NumOrder = NumOrder_;
            this.PatientType = PatientType_;
            this.ServiceReqId = ServiceReqId_;
        }
    }
}
