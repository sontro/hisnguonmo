using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisExamSchedule.ADO
{
    class DoctorADO
    {
        public string LoginName { get; set; }
        public string UseName { get; set; }

        public DoctorADO() { }
        public DoctorADO(string _LoginName, string _UseName)
        {
            this.LoginName = _LoginName;
            this.UseName = _UseName;
        }
    }
}
