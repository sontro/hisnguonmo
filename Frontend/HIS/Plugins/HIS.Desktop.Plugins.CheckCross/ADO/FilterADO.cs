using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CheckCross.ADO
{
    public class FilterADO
    {
        public long ID { get; set; }
        public string TYPE_CODE { get; set; }
        public string TYPE_NAME { get; set; }

        public FilterADO(long id, string code, string name)
        {
            this.ID = id;
            this.TYPE_CODE = code;
            this.TYPE_NAME = name;
        }
    }
}
