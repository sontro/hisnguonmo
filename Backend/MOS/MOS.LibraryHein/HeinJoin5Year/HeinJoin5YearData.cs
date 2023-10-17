
namespace MOS.LibraryHein.Bhyt.HeinJoin5Year
{
    public class HeinJoin5YearData
    {
        public string HeinJoin5YearName { get; set; }
        public string HeinJoin5YearCode { get; set; }

        public HeinJoin5YearData()
        {
        }

        public HeinJoin5YearData(string heinJoin5YearCode, string heinJoin5YearName)
        {
            HeinJoin5YearName = heinJoin5YearName;
            HeinJoin5YearCode = heinJoin5YearCode;
        }
    }
}
