using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestMaterialView2Filter : FilterBase
    {
        public List<long> EXP_MEST_IDs { get; set; }
        public List<long> MATERIAL_IDs { get; set; }
        public List<long> SERE_SERV_PARENT_IDs { get; set; }
        public long? SERE_SERV_PARENT_ID { get; set; }
        public long? MATERIAL_ID { get; set; }
        public long? EXP_MEST_ID { get; set; }
        public long? MATERIAL_TYPE_ID { get; set; }

        public HisExpMestMaterialView2Filter()
            : base()
        {
        }
    }
}
