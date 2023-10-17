
using System.Collections.Generic;
namespace MOS.Filter
{
    public class ActiveIngredientOrServiceId
	{
		public List<long> ServiceIds { get; set; }
		public List<long> ActiveIngredientIds { get; set; }
	}

    public class HisIcdServiceFilter : FilterBase
    {
        public long? SERVICE_ID { get; set; }
        public long? ACTIVE_INGREDIENT_ID { get; set; }

        public List<long> SERVICE_IDs { get; set; }
        public List<long> ACTIVE_INGREDIENT_IDs { get; set; }
        public string ICD_CODE__EXACT { get; set; }
        public List<string> ICD_CODE__EXACTs { get; set; }
        public ActiveIngredientOrServiceId SERVICE_ID_OR_ACTIVE_INGREDIENT_ID { get; set; }

        public HisIcdServiceFilter()
            : base()
        {
        }
    }
}
