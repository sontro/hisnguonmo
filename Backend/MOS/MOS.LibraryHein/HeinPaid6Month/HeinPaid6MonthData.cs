
namespace MOS.LibraryHein.Bhyt.HeinPaid6Month
{
    public class HeinPaid6MonthData
    {
        public string HeinPaid6MonthName { get; set; }
        public string HeinPaid6MonthCode { get; set; }

        public HeinPaid6MonthData()
        {
        }

        public HeinPaid6MonthData(string heinPaid6MonthCode, string heinPaid6MonthName)
        {
            HeinPaid6MonthName = heinPaid6MonthName;
            HeinPaid6MonthCode = heinPaid6MonthCode;
        }
    }
}
