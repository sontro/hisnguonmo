using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinRightRoute
{
    public class HeinRightRouteStore
    {
        private static readonly Dictionary<string, HeinRightRouteData> HEIN_RIGHT_ROUTE_STORE = new Dictionary<string, HeinRightRouteData>()
        {
            {HeinRightRouteCode.FALSE, new HeinRightRouteData(HeinRightRouteCode.FALSE, "Trái tuyến")},
            {HeinRightRouteCode.TRUE, new HeinRightRouteData(HeinRightRouteCode.TRUE, "Đúng tuyến")}
        };

        public static List<HeinRightRouteData> Get()
        {
            try
            {
                return HEIN_RIGHT_ROUTE_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinRightRouteData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_RIGHT_ROUTE_STORE.ContainsKey(code) ? HEIN_RIGHT_ROUTE_STORE[code] : null;
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
                LogSystem.Error("Ma 'Dung Tuyen' khong hop le");
                return false;
            }
            return true;
        }
    }
}
