using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class GetMaterialTypeForEmrResultSDO
    {
        public string MATERIAL_TYPE_CODE { get; set; }
        public string MATERIAL_TYPE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public long? SereServParentId { get; set; }
        public long? ServiceId { get; set; }
        public string ServiceName { get; set; }
        public long EXP_MEST_STT_ID { get; set; }
    }
}
