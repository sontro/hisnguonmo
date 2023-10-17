using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class SarPrintADO
    {
        public bool? IsFinished { get; set; }
        public string JSON_PRINT_ID { get; set; }
        public DelegateSelectData JsonPrintResult { get; set; }
    }
}
