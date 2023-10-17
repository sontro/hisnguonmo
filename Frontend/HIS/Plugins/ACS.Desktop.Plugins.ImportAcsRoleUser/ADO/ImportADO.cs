using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Desktop.Plugins.AcsImport.ADO
{
    public class ImportADO : ACS.EFMODEL.DataModels.ACS_ROLE_USER
    {
        public string ROLE_CODE { get; set; }
        public string LOGINNAME { get; set; }
        public string ROLE_NAME { get; set; }
        public string USERNAME { get; set; }
        public string ERROR { get; set; }
    }
}
