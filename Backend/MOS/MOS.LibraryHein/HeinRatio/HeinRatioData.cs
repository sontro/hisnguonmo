
namespace MOS.LibraryHein.Bhyt.HeinRatio
{
    class HeinRatioData
    {
        public string HeinBenefitCode { get; set; }
        public string HeinRatioTypeCode { get; set; }
        public string HeinLevel { get; set; }
        public string TreatmentTypeCode { get; set; }
        public string UpToStandardCode { get; set; }
        public string RightRouteCode { get; set; }
        public bool? NotOverLimit { get; set; }
        public decimal Ratio { get; set; }

        public HeinRatioData()
        {
        }

        public HeinRatioData(string heinBenefitCode, string heinRatioTypeCode, string heinLevel, string treatmentTypeCode, string upToStandardCode, string rightRouteCode, bool? notOverLimit, decimal ratio)
        {
            this.HeinBenefitCode = heinBenefitCode;
            this.HeinRatioTypeCode = heinRatioTypeCode;
            this.HeinLevel = heinLevel;
            this.TreatmentTypeCode = treatmentTypeCode;
            this.UpToStandardCode = upToStandardCode;
            this.RightRouteCode = rightRouteCode;
            this.NotOverLimit = notOverLimit;
            this.Ratio = ratio;
        }
    }
}
