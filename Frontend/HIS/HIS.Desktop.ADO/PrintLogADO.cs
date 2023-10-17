using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class PrintLogADO
    {
        public string PRINT_TYPE_CODE { get; set; }
        public string UNIQUE_CODE { get; set; }

        public PrintLogADO(string printTypeCode, string uniqueCode)
        {
            this.PRINT_TYPE_CODE = printTypeCode;
            this.UNIQUE_CODE = uniqueCode;
        }

    }
}
