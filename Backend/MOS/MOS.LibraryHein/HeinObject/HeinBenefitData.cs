using System.Collections.Generic;

namespace MOS.LibraryHein.Bhyt.HeinObject
{
    class HeinBenefitData
    {
        public string HeinBenefitCode { get; set; }
        public List<string> ProperHeinObjectCodes { get; set; }
        public bool IsLimitHighServicePriceTotal { get; set; }
        public bool IsLimitServicePrice { get; set; }

        public HeinBenefitData()
        {
        }

        public HeinBenefitData(string heinBenefitCode, List<string> properHeinObjectCodes, bool isLimitHighServicePriceTotal, bool isLimitServicePrice)
        {
            this.HeinBenefitCode = heinBenefitCode;
            this.ProperHeinObjectCodes = properHeinObjectCodes;
            this.IsLimitHighServicePriceTotal = isLimitHighServicePriceTotal;
            this.IsLimitServicePrice = isLimitServicePrice;
        }
    }
}
