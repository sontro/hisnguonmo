
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisMedicineMaterialViewFilter : FilterBase
    {
        public long? MEDICINE_ID { get; set; }
        public long? MATERIAL_ID { get; set; }

        public List<long> MEDICINE_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }

        public long? MATERIAL_TYPE_ID { get; set; }
        public long? SUPPLIER_ID { get; set; }

        public List<long> MATERIAL_TYPE_IDs { get; set; }
        public List<long> SUPPLIER_IDs { get; set; }

        public HisMedicineMaterialViewFilter()
            : base()
        {
        }
    }
}
