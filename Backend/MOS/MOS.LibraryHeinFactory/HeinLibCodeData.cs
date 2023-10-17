using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.LibraryHein.Factory
{
    public class HeinLibCodeData
    {
        public const string BHYT = "BHYT";
        public const string AIA = "AIA";

        public string HeinLibCode { get; set; }
        public string HeinLibName { get; set; }

        public HeinLibCodeData(string heinLibCode, string heinLibName)
        {
            this.HeinLibCode = heinLibCode;
            this.HeinLibName = heinLibName;
        }

        public static List<HeinLibCodeData> GetLibCodes()
        {
            return new List<HeinLibCodeData>() {
                new HeinLibCodeData(HeinLibCodeData.BHYT, "BHYT"),
                new HeinLibCodeData(HeinLibCodeData.AIA, "AIA")
            };
        }
    }
}
