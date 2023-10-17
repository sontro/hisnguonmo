using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MOS.MANAGER.Config
{
    public class HisPoorFundCFG
    {
        /// <summary>
        /// Co ap dung quy ho ngheo hay khong
        /// </summary>
        private const string IS_APPLIED_CFG = "MOS.POOR_FUND.IS_APPLIED";

        private static bool? isApplied;
        public static bool IS_APPLIED
        {
            get
            {
                if (!isApplied.HasValue)
                {
                    isApplied = ConfigUtil.GetIntConfig(IS_APPLIED_CFG) == 1;
                }
                return isApplied.Value;
            }
        }

        public static void Reload()
        {
            isApplied = ConfigUtil.GetIntConfig(IS_APPLIED_CFG) == 1;
        }
    }
}
