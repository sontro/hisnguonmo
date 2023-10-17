
namespace SDA.Filter
{
    public class SdaTranslateFilter : FilterBase
    {
        public long? LANGUAGE_ID { get; set; }
        public string SCHEMA { get; set; }
        public string TABLE_NAME { get; set; }

        public SdaTranslateFilter()
            : base()
        {
        }
    }
}
