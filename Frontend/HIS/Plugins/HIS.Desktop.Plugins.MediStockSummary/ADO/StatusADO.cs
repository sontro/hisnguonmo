using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MediStockSummary.ADO
{
    class StatusADO
    {
        public string ID { get; set; }
        public string STATUS_NAME { get; set; }
        public StatusADO(string id, string st)
        {
            this.ID = id;
            this.STATUS_NAME = st;
        }

    }
}
