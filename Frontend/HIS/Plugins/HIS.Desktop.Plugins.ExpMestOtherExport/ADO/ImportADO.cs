using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestOtherExport.ADO
{
    public class ImportADO
    {
        public long TYPE_ID { get; set; }
        public string TYPE_CODE { get; set; }
        public decimal AMOUNT { get; set; }
        public string MESSAGE_ERROR { get; set; }
        public List<string> MessageErrors = new List<string>();
    }
}
