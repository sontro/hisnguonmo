using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinLevel
{
    public class HeinLevelStore
    {
        private static readonly Dictionary<string, HeinLevelData> HEIN_LEVEL_STORE = new Dictionary<string, HeinLevelData>()
        {
            {HeinLevelCode.DISTRICT, new HeinLevelData(HeinLevelCode.DISTRICT, "Huyện")},
            {HeinLevelCode.PROVINCE, new HeinLevelData(HeinLevelCode.PROVINCE, "Tỉnh")},
            {HeinLevelCode.NATIONAL, new HeinLevelData(HeinLevelCode.NATIONAL, "Trung ương")},
            {HeinLevelCode.COMMUNE, new HeinLevelData(HeinLevelCode.COMMUNE, "Xã")}
        };

        public static List<HeinLevelData> Get()
        {
            try
            {
                return HEIN_LEVEL_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinLevelData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_LEVEL_STORE.ContainsKey(code) ? HEIN_LEVEL_STORE[code] : null;
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
                LogSystem.Error("Ma 'Tuyen Benh vien (trung uong/tinh/huyen/xa)' khong hop le: " + code);
                return false;
            }
            return true;
        }
    }
}
