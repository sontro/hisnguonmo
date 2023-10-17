using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinJoin5Year
{
    public class HeinJoin5YearStore
    {
        public static readonly Dictionary<string, HeinJoin5YearData> HEIN_JOIN_5_YEAR_STORE = new Dictionary<string, HeinJoin5YearData>()
        {
            {HeinJoin5YearCode.TRUE, new HeinJoin5YearData(HeinJoin5YearCode.TRUE, "Tham gia bảo hiểm đủ 5 năm")},
            {HeinJoin5YearCode.FALSE, new HeinJoin5YearData(HeinJoin5YearCode.FALSE, "Chưa đạt")}
        };

        public static List<HeinJoin5YearData> Get()
        {
            try
            {
                return HEIN_JOIN_5_YEAR_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinJoin5YearData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_JOIN_5_YEAR_STORE.ContainsKey(code) ? HEIN_JOIN_5_YEAR_STORE[code] : null;
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
                LogSystem.Error("Ma 'Dat chuan' khong hop le");
                return false;
            }
            return true;
        }
    }
}
