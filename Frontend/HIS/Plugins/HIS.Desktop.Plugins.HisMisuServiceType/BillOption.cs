using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMisuServiceType
{
    public class BillOption
    {
        public long id { get; set; }
        public string billName { get; set; }

        public BillOption(long id, string billName)
        {
            this.id = id;
            this.billName = billName;
        }
    }
}
