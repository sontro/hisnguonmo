using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintPublicMedicines.ADO
{
    public class Service_NT_ADO
    {
        public string SERVICE_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public long SERVICE_ID { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string SERVICE_UNIT_CODE { get; set; }
        public long SERVICE_UNIT_ID { get; set; }
        public decimal AMOUNT { get; set; }
        public decimal? PRICE { get; set; }
        public string DESCRIPTION { get; set; }
        public long SERVICE_TYPE_ID { get; set; }
        public long INTRUCTION_DATE { get; set; }
        public string CONCENTRA { get; set; }
        public long REQUEST_DEPARTMENT_ID { get; set; }
        public string REQUEST_DEPARTMENT_NAME { get; set; }
    }
}
