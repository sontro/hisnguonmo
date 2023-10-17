using System.Collections.Generic;

namespace MOS.Filter
{
    public class HisExpMestTutorialFilter
    {
        public long TREATMENT_ID { get; set; }
        public bool INCLUDE_MATERIAL { get; set; }
        public List<long> EXP_MEST_TYPE_IDs { get; set; }
        public List<long> MEDICINE_USE_FORM_IDs { get; set; }

        public HisExpMestTutorialFilter()
            : base()
        {
        }
    }
}
