using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentIcdEdit.ADO
{
    public class DoctorADO
    {
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }

        public DoctorADO() { }

        public DoctorADO(ACS.EFMODEL.DataModels.ACS_USER data)
        {
            if (data != null)
            {
                this.LOGINNAME = data.LOGINNAME;
                this.USERNAME = data.USERNAME;
            }
        }
    }
}
