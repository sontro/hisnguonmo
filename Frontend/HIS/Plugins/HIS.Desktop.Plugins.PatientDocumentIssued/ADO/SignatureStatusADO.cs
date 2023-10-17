using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PatientDocumentIssued.ADO
{
    class SignatureStatusADO
    {
        public long ID { get; set; }
        public string StatusName { get; set; }
        public SignatureStatusADO(long _ID, string _StatusName)
        {
            this.ID = _ID;
            this.StatusName = _StatusName;
        }
    }
}
