using Inventec.Common.Logging;
using MOS.LibraryHein.Bhyt.HeinJoin5Year;
using MOS.LibraryHein.Bhyt.HeinPaid6Month;
using MOS.LibraryHein.Bhyt.HeinRightRouteType;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.LibraryHein.Bhyt.HeinUpToStandard
{
    class HeinUpToStandardStore
    {
        /// <summary>
        /// Huong che do danh cho the du "5 nam 6 thang" chi tinh voi cac the co check dat 5 nam + check dat 6 thang va kham/dieu tri dung tuyen
        /// </summary>
        /// <param name="heinJoin5Year"></param>
        /// <param name="heinPaid6Month"></param>
        /// <param name="mediOrgCode"></param>
        /// <param name="acceptedMediOrgCodes"></param>
        /// <returns></returns>
        public static string GetHeinUpToStandardCode(string heinJoin5Year, string heinPaid6Month, string rightRouteTypeCode, string mediOrgCode, List<string> acceptedMediOrgCodes)
        {
            return heinJoin5Year == HeinJoin5YearCode.TRUE
                && heinPaid6Month == HeinPaid6MonthCode.TRUE
                && mediOrgCode != null && acceptedMediOrgCodes != null
                && (acceptedMediOrgCodes.Contains(mediOrgCode) || rightRouteTypeCode != HeinRightRouteTypeCode.EMERGENCY) ?
                HeinUpToStandardCode.TRUE : HeinUpToStandardCode.FALSE;
            //return heinJoin5Year == HeinJoin5YearCode.TRUE && heinPaid6Month == HeinPaid6MonthCode.TRUE ? HeinUpToStandardCode.TRUE : HeinUpToStandardCode.FALSE;
        }
    }
}
