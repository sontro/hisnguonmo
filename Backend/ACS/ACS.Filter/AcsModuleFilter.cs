
namespace ACS.Filter
{
    public class AcsModuleFilter : FilterBase
    {
        public bool? IsLeaf { get; set; }
        public bool? IsParent { get; set; }
        public long? APPLICATION_ID { get; set; }
        public string MODULE_LINK { get; set; }

        public AcsModuleFilter()
            : base()
        {
        }
    }
}
