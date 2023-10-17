using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class UserSchedulerJobSDO
    {
        public string JOB_NAME { get; set; }
        public string ENABLED { get; set; }
        public string REPEAT_INTERVAL { get; set; }
        public DateTime? LAST_START_DATE { get; set; }
        public TimeSpan? LAST_RUN_DURATION { get; set; }
        public DateTime? NEXT_RUN_DATE { get; set; }
        public decimal? RUN_COUNT { get; set; }
        public decimal? FAILURE_COUNT { get; set; }
        public DateTime? START_DATE { get; set; }
        public string COMMENTS { get; set; }
    }

    public class UserSchedulerJobResultSDO
    {
        public string JOB_NAME { get; set; }
        public bool ENABLED { get; set; }
        public string REPEAT_INTERVAL { get; set; }
        public long? LAST_START_DATE { get; set; }
        public string LAST_RUN_DURATION { get; set; }
        public long? NEXT_RUN_DATE { get; set; }
        public decimal? RUN_COUNT { get; set; }
        public decimal? FAILURE_COUNT { get; set; }
        public long? START_DATE { get; set; }
        public string COMMENTS { get; set; }
    }
}
