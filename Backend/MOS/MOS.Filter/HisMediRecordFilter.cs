
namespace MOS.Filter
{
    public class HisMediRecordFilter : FilterBase
    {
        public decimal? VIR_STORE_YEAR__EQUAL { get; set; }
        public decimal? VIR_SEED_CODE_YEAR__EQUAL { get; set; }
        public long? PATIENT_ID { get; set; }
        public long? PROGRAM_ID { get; set; }
        public long? DATA_STORE_ID { get; set; }

        public string STORE_CODE__END_WITH { get; set; }
        public string STORE_CODE__START_WITH { get; set; }

        public string STORE_CODE { get; set; }

        public HisMediRecordFilter()
            : base()
        {
        }
    }
}
