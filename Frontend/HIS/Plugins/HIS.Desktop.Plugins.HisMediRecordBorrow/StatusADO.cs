using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMediRecordBorrow
{
    public class StatusADO
    {
        public string ID { get; set; }
        public string NAME { get; set; }

        public StatusADO(string id, string name)
        {
            this.ID = id;
            this.NAME = name;
        }
    }
}
