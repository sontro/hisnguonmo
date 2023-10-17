
namespace MOS.LibraryHein.Bhyt.HeinLevel
{
    public class HeinLevelData
    {
        public string HeinLevelName { get; set; }
        public string HeinLevelCode { get; set; }

        public HeinLevelData()
        {
        }

        public HeinLevelData(string heinLevelCode, string heinLevelName)
        {
            HeinLevelName = heinLevelName;
            HeinLevelCode = heinLevelCode;
        }
    }
}
