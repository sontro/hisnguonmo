
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaccinationVrplFilter : FilterBase
    {
        public long? VACCINATION_ID { get; set; }
        public long? VACC_REACT_PLACE_ID { get; set; }

        public List<long> VACCINATION_IDs { get; set; }
        public List<long> VACC_REACT_PLACE_IDs { get; set; }

        public HisVaccinationVrplFilter()
            : base()
        {
        }
    }
}
