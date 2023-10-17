using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ContentSubclinicalADO
    {
        public long TDL_SERVICE_TYPE_ID { get; set; }
        public string TDL_SERVICE_CODE { get; set; }
        public string TDL_SERVICE_NAME { get; set; }
        public string TEST_INDEX_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public long? NUM_ORDER { get; set; }
        public string VALUE { get; set; }
        public string DESCRIPTION { get; set; }
        public long SERVICE_ID { get; set; }
    }
}
