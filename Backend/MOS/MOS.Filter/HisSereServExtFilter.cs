
using System.Collections.Generic;
namespace MOS.Filter
{
    public class HisSereServExtFilter : FilterBase
    {
        public long? SERE_SERV_ID { get; set; }
        public long? MACHINE_ID { get; set; }
        public long? TDL_SERVICE_REQ_ID { get; set; }
        public long? TDL_TREATMENT_ID { get; set; }
        public long? FILM_SIZE_ID { get; set; }

        public List<long> SERE_SERV_IDs { get; set; }
        public List<long> MACHINE_IDs { get; set; }
        public List<long> TDL_SERVICE_REQ_IDs { get; set; }
        public List<long> TDL_TREATMENT_IDs { get; set; }
        public List<long> FILM_SIZE_IDs { get; set; }

        public HisSereServExtFilter()
            : base()
        {
        }
    }
}
