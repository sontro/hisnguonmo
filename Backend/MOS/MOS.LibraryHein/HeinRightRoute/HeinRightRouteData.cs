
namespace MOS.LibraryHein.Bhyt.HeinRightRoute
{
    public class HeinRightRouteData
    {
        public string HeinRightRouteName { get; set; }
        public string HeinRightRouteCode { get; set; }

        public HeinRightRouteData()
        {
        }

        public HeinRightRouteData(string heinRightRouteCode, string heinRightRouteName)
        {
            HeinRightRouteName = heinRightRouteName;
            HeinRightRouteCode = heinRightRouteCode;
        }
    }
}
