using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisDhst.ADO
{
    public class MessFunctionADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public MessFunctionADO(long _ID, string _NAME)
        {
            this.ID = _ID;
            this.NAME = _NAME;
        }
    }
}
