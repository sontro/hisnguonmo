
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisImpMestMaterialFilter : FilterBase
    {
        public List<long> MATERIAL_IDs { get; set; }
        public List<long> IMP_MEST_IDs { get; set; }
        public List<string> SERIAL_NUMBERs { get; set; }
        public long? MATERIAL_ID { get; set; }
        public long? IMP_MEST_ID { get; set; }
        public string SERIAL_NUMBER__EXACT { get; set; }


        public long? IMP_MEST_ID__NOT__EQUAL { get; set; }

        public HisImpMestMaterialFilter()
            : base()
        {
        }
    }
}
