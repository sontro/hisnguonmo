
namespace MOS.LibraryHein.Bhyt.HeinUpToStandard
{
    class HeinUpToStandardData
    {
        public string HeinUpToStandardName { get; set; }
        public string HeinUpToStandardCode { get; set; }

        public HeinUpToStandardData()
        {
        }

        public HeinUpToStandardData(string heinUpToStandardCode, string heinUpToStandardName)
        {
            HeinUpToStandardName = heinUpToStandardName;
            HeinUpToStandardCode = heinUpToStandardCode;
        }
    }
}
