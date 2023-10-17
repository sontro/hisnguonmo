using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InfantInformationList.ADO
{
    class ConnectionInfoADO
    {
        public string BranchCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Url { get; set; }
        public string Certificate { get; set; }
        public string PassCert { get; set; }
        public ConnectionInfoADO()
        {

        }

    }
}
