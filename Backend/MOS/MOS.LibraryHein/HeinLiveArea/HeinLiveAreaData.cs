
namespace MOS.LibraryHein.Bhyt.HeinLiveArea
{
    public class HeinLiveAreaData
    {
        public string HeinLiveName { get; set; }
        public string HeinLiveCode { get; set; }

        public HeinLiveAreaData()
        {
        }

        public HeinLiveAreaData(string heinLiveName, string heinLiveCode)
        {
            HeinLiveName = heinLiveName;
            HeinLiveCode = heinLiveCode;
        }
    }
}
