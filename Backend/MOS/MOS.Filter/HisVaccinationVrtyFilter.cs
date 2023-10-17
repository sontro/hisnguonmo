
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisVaccinationVrtyFilter : FilterBase
    {
        public long? VACCINATION_ID { get; set; }
        public long? VACC_REACT_TYPE_ID { get; set; }

        public List<long> VACCINATION_IDs { get; set; }
        public List<long> VACC_REACT_TYPE_IDs { get; set; }

        public HisVaccinationVrtyFilter()
            : base()
        {
        }
    }
}
