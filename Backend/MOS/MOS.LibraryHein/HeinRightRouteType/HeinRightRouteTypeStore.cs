using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinRightRouteType
{
    public class HeinRightRouteTypeStore
    {
        private static readonly Dictionary<string, HeinRightRouteTypeData> HEIN_RIGHT_ROUTE_TYPE_STORE = new Dictionary<string, HeinRightRouteTypeData>()
        {
            {HeinRightRouteTypeCode.EMERGENCY, new HeinRightRouteTypeData(HeinRightRouteTypeCode.EMERGENCY, "Cấp cứu")},
            {HeinRightRouteTypeCode.PRESENT, new HeinRightRouteTypeData(HeinRightRouteTypeCode.PRESENT, "Giới thiệu")},
            {HeinRightRouteTypeCode.APPOINTMENT, new HeinRightRouteTypeData(HeinRightRouteTypeCode.APPOINTMENT, "Hẹn khám")},
            {HeinRightRouteTypeCode.OVER, new HeinRightRouteTypeData(HeinRightRouteTypeCode.OVER, "Thông tuyến")}
        };

        public static List<HeinRightRouteTypeData> Get()
        {
            try
            {
                return HEIN_RIGHT_ROUTE_TYPE_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinRightRouteTypeData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_RIGHT_ROUTE_TYPE_STORE.ContainsKey(code) ? HEIN_RIGHT_ROUTE_TYPE_STORE[code] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static bool IsValidCode(string code)
        {
            if (GetByCode(code) == null)
            {
                LogSystem.Error("Ma 'Loai Dung tuyen' khong hop le");
                return false;
            }
            return true;
        }
    }
}
