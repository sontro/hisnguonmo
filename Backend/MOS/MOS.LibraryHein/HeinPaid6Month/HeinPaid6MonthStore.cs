using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinPaid6Month
{
    public class HeinPaid6MonthStore
    {
        private static readonly Dictionary<string, HeinPaid6MonthData> HEIN_PAID_6_MONTH_STORE = new Dictionary<string, HeinPaid6MonthData>()
        {
            {HeinPaid6MonthCode.TRUE, new HeinPaid6MonthData(HeinPaid6MonthCode.TRUE, "Đồng chi trả lũy kế đủ 6 tháng")},
            {HeinPaid6MonthCode.FALSE, new HeinPaid6MonthData(HeinPaid6MonthCode.FALSE, "Chưa đạt")}
        };

        public static List<HeinPaid6MonthData> Get()
        {
            try
            {
                return HEIN_PAID_6_MONTH_STORE.Values.ToList();
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        public static HeinPaid6MonthData GetByCode(string code)
        {
            try
            {
                return code != null && HEIN_PAID_6_MONTH_STORE.ContainsKey(code) ? HEIN_PAID_6_MONTH_STORE[code] : null;
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
                LogSystem.Error("Ma 'Dong chi tra 6 thang' khong hop le");
                return false;
            }
            return true;
        }
    }
}
