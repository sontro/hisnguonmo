
namespace SDA.Filter
{
    public class SdaEventLogFilter : FilterBase
    {
        public string LOGIN_NAME { get; set; }

        public string DESCRIPTION { get; set; }

        public long? EVENT_TIME_FROM { get; set; }
        public long? EVENT_TIME_TO { get; set; }

        public long? EVENT_DATE_FROM { get; set; }
        public long? EVENT_DATE_TO { get; set; }

        public long? EVENT_DATE { get; set; }

        public long? EVENT_MONTH { get; set; }

        public long? CREATE_DATE_FROM { get; set; }
        public long? CREATE_DATE_TO { get; set; }

        public SdaEventLogFilter()
            : base()
        {
        }
    }
}
