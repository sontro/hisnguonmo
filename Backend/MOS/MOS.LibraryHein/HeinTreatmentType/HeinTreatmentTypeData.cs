
namespace MOS.LibraryHein.Bhyt.HeinTreatmentType
{
    public class HeinTreatmentTypeData
    {
        public string HeinTreatmentTypeName { get; set; }
        public string HeinTreatmentTypeCode { get; set; }

        public HeinTreatmentTypeData()
        {
        }

        public HeinTreatmentTypeData(string heinTreatmentTypeName, string heinTreatmentTypeCode)
        {
            HeinTreatmentTypeName = heinTreatmentTypeName;
            HeinTreatmentTypeCode = heinTreatmentTypeCode;
        }
    }
}
