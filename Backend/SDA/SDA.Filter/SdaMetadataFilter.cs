
namespace SDA.Filter
{
    public class SdaMetadataFilter : FilterBase
    {
        public string SCHEMA_NAME__EXACT { get; set; }
        public string TABLE_NAME__EXACT { get; set; }

        public SdaMetadataFilter()
            : base()
        {
        }
    }
}
