
namespace SDA.Filter
{
    public class SdaModuleFieldFilter : FilterBase
    {
        public SdaModuleFieldFilter()
            : base()
        {
        }

        public string MODULE_LINK { get; set; }
        public short? IS_VISIBLE { get; set; }
    }
}
