using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Vaccination.ADO
{
    class StatusADO
    {
        public string ID { get; set; }
        public string STATUS { get; set; }

        public StatusADO(string id, string status)
        {
            this.ID = id;
            this.STATUS = status;
        }
    }
}
