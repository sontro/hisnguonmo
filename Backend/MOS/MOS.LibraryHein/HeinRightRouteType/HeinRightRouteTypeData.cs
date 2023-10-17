
namespace MOS.LibraryHein.Bhyt.HeinRightRouteType
{
    public class HeinRightRouteTypeData
    {
        public string HeinRightRouteTypeName { get; set; }
        public string HeinRightRouteTypeCode { get; set; }

        public HeinRightRouteTypeData()
        {
        }

        public HeinRightRouteTypeData(string heinRightRouteTypeCode, string heinRightRouteTypeName)
        {
            HeinRightRouteTypeName = heinRightRouteTypeName;
            HeinRightRouteTypeCode = heinRightRouteTypeCode;
        }
    }
}
