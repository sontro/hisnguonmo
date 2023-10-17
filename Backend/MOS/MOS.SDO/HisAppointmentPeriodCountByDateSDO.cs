using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAppointmentPeriodCountByDateSDO
    {
        public long ID { get; set; }
        public Nullable<long> FROM_HOUR { get; set; }
        public Nullable<long> FROM_MINUTE { get; set; }
        public Nullable<long> TO_HOUR { get; set; }
        public Nullable<long> TO_MINUTE { get; set; }
        public Nullable<long> APPOINTMENT_DATE { get; set; }
        public long BRANCH_ID { get; set; }
        public long? MAXIMUM { get; set; }
        public long CURRENT_COUNT { get; set; }
    }
}
