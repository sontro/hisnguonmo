using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinLiveArea
{
    public class HeinLiveAreaStore
    {
        private static readonly Dictionary<string, HeinLiveAreaData> HEIN_LIVE_AREA_STORE = new Dictionary<string, HeinLiveAreaData>()
        {
            {HeinLiveAreaCode.K1, new HeinLiveAreaData(HeinLiveAreaCode.K1, HeinLiveAreaCode.K1)},
            {HeinLiveAreaCode.K2, new HeinLiveAreaData(HeinLiveAreaCode.K2, HeinLiveAreaCode.K2)},
            {HeinLiveAreaCode.K3, new HeinLiveAreaData(HeinLiveAreaCode.K3, HeinLiveAreaCode.K3)}
        };

        public static List<HeinLiveAreaData> Get()
        {
            try
            {
                return HEIN_LIVE_AREA_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinLiveAreaData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_LIVE_AREA_STORE.ContainsKey(code) ? HEIN_LIVE_AREA_STORE[code] : null;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static List<string> GetCodes()
        {
            try
            {
                return HEIN_LIVE_AREA_STORE.Keys.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static bool IsValidCode(string heinLiveAreaCode)
        {
            if (!GetCodes().Contains(heinLiveAreaCode))
            {
                LogSystem.Error("Ma 'Khu vuc song' khong hop le");
                return false;
            }
            return true;
        }
    }
}
