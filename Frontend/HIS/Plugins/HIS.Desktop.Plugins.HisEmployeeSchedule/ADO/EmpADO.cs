using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisEmployeeSchedule.ADO
{
    public class EmpADO
    {
        public string LOGINNAME { get; set; }
        public string USERNAME { get; set; }
        public string DisplayName {
            get
            {
                return this.LOGINNAME + " - " + this.USERNAME;
            }
        }
    }
}
