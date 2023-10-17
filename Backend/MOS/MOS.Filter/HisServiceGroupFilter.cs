
namespace MOS.Filter
{
    public class HisServiceGroupFilter : FilterBase
    {
        public bool? CAN_VIEW { get; set; }

        public bool? CAN_VIEW_ACTIVE { get; set; }
        /// <summary>
        /// công khai
        /// </summary>
        public short? IS_PUBLIC { get; set; }
        public HisServiceGroupFilter()
            : base()
        {

        }
    }
}
