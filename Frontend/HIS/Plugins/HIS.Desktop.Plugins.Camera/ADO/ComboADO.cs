using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Camera.ADO
{
    public class ComboADO
    {
        public int id { get; set; }
        public string statusName { get; set; }

        public ComboADO() { }
        public ComboADO(int id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }
    }
}
