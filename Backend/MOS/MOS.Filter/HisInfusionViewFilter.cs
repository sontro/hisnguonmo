using System;
using System.Collections.Generic;
using System.Text;

namespace MOS.Filter
{
    public class HisInfusionViewFilter : FilterBase
    {
        public List<long> INFUSION_SUM_IDs { get; set; }
        public List<long> MEDICINE_IDs { get; set; }
        public long? INFUSION_SUM_ID { get; set; }
        public long? MEDICINE_ID { get; set; }
        public List<long> TREATMENT_IDs { get; set; }
        public List<long> DEPARTMENT_IDs { get; set; }
        public List<long> ROOM_IDs { get; set; }
        public long? TREATMENT_ID { get; set; }
        public long? DEPARTMENT_ID { get; set; }
        public long? ROOM_ID { get; set; }
        public long? START_TIME_FROM { get; set; }
        public long? START_TIME_TO { get; set; }

        public HisInfusionViewFilter()
            : base()
        {
        }
    }
}
