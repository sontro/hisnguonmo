using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisSampleList.ADO
{
    class ComboADO
    {
        public long id { get; set; }
        public string statusName { get; set; }

        public ComboADO() { }
        public ComboADO(long id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }
    }
}
