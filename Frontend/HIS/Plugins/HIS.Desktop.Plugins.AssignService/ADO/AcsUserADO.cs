using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.ADO
{
    public class AcsUserADO : ACS.EFMODEL.DataModels.ACS_USER
    {
        public string DEPARTMENT_NAME { get; set; }
        public string DOB { get; set; }
        public string DIPLOMA { get; set; }
    }
}
