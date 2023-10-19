
namespace ACS.Filter
{
    public class AcsModuleViewFilter : FilterBase
    {
        public bool? IsTree { get; set; }
        public long? Node { get; set; }
        public long? APPLICATION_ID { get; set; }
        public string APPLICATION_CODE { get; set; }
        public string MODULE_LINK { get; set; }
        public short? IS_LEAF { get; set; }
        public short? IS_VISIBLE { get; set; }
        public bool? IsLeaf { get; set; }
        public bool? IsParent { get; set; }
        public bool? IsAnonymous { get; set; }

        public AcsModuleViewFilter()
            : base()
        {
        }
    }
}
