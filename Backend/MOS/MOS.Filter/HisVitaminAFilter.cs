
namespace MOS.Filter
{
    public class HisVitaminAFilter : FilterBase
    {
        public long? BRANCH_ID { get; set; }
        public long? EXP_MEST_ID { get; set; }

        public bool? HasExecuteTime { get; set; }
        public bool? HasMedicineTypeId { get; set; }

        public HisVitaminAFilter()
            : base()
        {
        }
    }
}
