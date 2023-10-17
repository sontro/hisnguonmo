using MOS.MANAGER.Config;
using System;

namespace MOS.MANAGER.HisTransReq
{
    class HisTransReqUtil
    {
        public static decimal RoundAmount(decimal value)
        {
            decimal result = 0;
            var option = HisTransReqCFG.AUTO_ROUND_AMOUNT_OPTION;
            switch (option)
            {
                case 1 :
                    result = Math.Round(value, MidpointRounding.AwayFromZero);
                    break;
                default:
                    result = Math.Ceiling(value);
                    break;
            }
            return result;
        }
    }
}
