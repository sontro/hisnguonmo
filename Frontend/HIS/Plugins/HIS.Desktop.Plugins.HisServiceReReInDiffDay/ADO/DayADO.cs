using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceReReInDiffDay.ADO
{
    class DayADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public DayADO() { }
        public DayADO(long id, string name) 
        {
            this.ID = id;
            this.NAME = name;
        }
    }
}
