using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.ADO
{
    class StatusADO
    {
        public enum Status
        {
            DaTao,
            ChuaTao,
            TatCa
        }

        public Status ID { get; set; }
        public string NAME { get; set; }

        public StatusADO(Status id, string name)
        {
            this.ID = id;
            this.NAME = name;
        }
    }
}
