using MOS.EFMODEL.DataModels;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisSereServ
{
    /// <summary>
    /// Tinh toan dua theo cong thuc cac cot virtual trong DB
    /// Luu y: 
    /// - Neu DB sua cong thuc thi can sua lai cac ham o day
    /// - Cac cho co thuc hien lam tron can chon MidpointRounding.AwayFromZero de dam bao neu le la .5 thi se lam tron len (de khop voi cong thuc lam trong trong DB).
    /// Neu ko co tham so nay thi mac dinh se lam tron xuong (.5 --> lam tron ve 0), dan den sai lech voi DB
    /// </summary>
    class SereServVirtualColumn
    {
        public static decimal VIR_TOTAL_PATIENT_PRICE_NO_DC(HIS_SERE_SERV s)
        {
            if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE)
            {
                return 0;
            }
            else if (!s.HEIN_PRICE.HasValue)
            {
                decimal heinRatio = s.HEIN_RATIO.HasValue ? Math.Round(s.HEIN_RATIO.Value, 4, MidpointRounding.AwayFromZero) : 0;
                return s.AMOUNT * Math.Round((s.PRICE + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) - s.AMOUNT * Math.Round(s.PRICE * (1 + s.VAT_RATIO) * heinRatio) - Math.Round(CommonUtil.NVL(s.OTHER_SOURCE_PRICE, 0) * s.AMOUNT, 4, MidpointRounding.AwayFromZero);
            }
            else
            {
                return s.AMOUNT * Math.Round((s.PRICE + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) - s.AMOUNT * Math.Round(s.HEIN_PRICE.Value, 4, MidpointRounding.AwayFromZero) - Math.Round(CommonUtil.NVL(s.OTHER_SOURCE_PRICE, 0) * s.AMOUNT, 4, MidpointRounding.AwayFromZero);
            }
        }

        public static decimal VIR_TOTAL_PATIENT_PRICE(HIS_SERE_SERV s)
        {
            decimal discount = s.DISCOUNT.HasValue ? s.DISCOUNT.Value : 0;
            return VIR_TOTAL_PATIENT_PRICE_NO_DC(s) - discount;
        }

        public static decimal VIR_HEIN_PRICE(HIS_SERE_SERV s)
        {
            if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE)
            {
                return 0;
            }
            else if (!s.HEIN_PRICE.HasValue)
            {
                decimal heinRatio = s.HEIN_RATIO.HasValue ? Math.Round(s.HEIN_RATIO.Value, 4, MidpointRounding.AwayFromZero) : 0;
                return Math.Round(s.PRICE * (1 + s.VAT_RATIO) * heinRatio, 4, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round(s.HEIN_PRICE.Value, 4);
            }
        }

        public static decimal VIR_PRICE(HIS_SERE_SERV s)
        {
            if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE)
            {
                return 0;
            }
            else
            {
                return Math.Round((s.PRICE + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero);
            }
        }

        public static decimal? VIR_LIMIT_PRICE(HIS_SERE_SERV s)
        {
            if (s.LIMIT_PRICE.HasValue)
            {
                if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE)
                {
                    return 0;
                }
                else
                {
                    return Math.Round((s.LIMIT_PRICE.Value + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero);
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// So tien BN phai tra theo gia BHYT, ma chua tru di so tien "quy khac chi tra"
        /// </summary>
        /// <returns></returns>
        public static decimal VIR_PATIENT_PRICE_BHYT_NO_OTHER_SOURCE(HIS_SERE_SERV s)
        {
            if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE || s.HEIN_CARD_NUMBER == null)
            {
                return 0;
            }
            if (s.PATIENT_PRICE_BHYT.HasValue)
            {
                return Math.Round(s.PATIENT_PRICE_BHYT.Value, 4, MidpointRounding.AwayFromZero);
            }
            if (!s.HEIN_PRICE.HasValue && !s.HEIN_LIMIT_PRICE.HasValue)
            {
                return Math.Round(s.PRICE * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) * (1 - (s.HEIN_RATIO ?? 0));
            }
            if (!s.HEIN_PRICE.HasValue && s.HEIN_LIMIT_PRICE.HasValue)
            {
                return Math.Round(s.HEIN_LIMIT_PRICE.Value, 4, MidpointRounding.AwayFromZero) * (1 - (s.HEIN_RATIO ?? 0));
            }
            if (s.HEIN_PRICE.HasValue && s.HEIN_LIMIT_PRICE.HasValue)
            {
                return Math.Round(s.HEIN_LIMIT_PRICE.Value, 4, MidpointRounding.AwayFromZero) - Math.Round(s.HEIN_PRICE.Value, 4);
            }
            return Math.Round(s.PRICE * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) - Math.Round(s.HEIN_PRICE.Value, 4);
        }

        public static decimal VIR_PATIENT_PRICE_NO_OTHER_SOURCE(HIS_SERE_SERV s)
        {
            if (s.IS_EXPEND == Constant.IS_TRUE || s.IS_NO_EXECUTE == Constant.IS_TRUE)
            {
                return 0;
            }
            if (!s.HEIN_PRICE.HasValue)
            {
                decimal heinRatio = s.HEIN_RATIO.HasValue ? Math.Round(s.HEIN_RATIO.Value, 4, MidpointRounding.AwayFromZero) : 0;
                return Math.Round((s.PRICE + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) - Math.Round(s.PRICE * (1 + s.VAT_RATIO) * heinRatio, 4, MidpointRounding.AwayFromZero);
            }
            else
            {
                return Math.Round((s.PRICE + CommonUtil.NVL(s.ADD_PRICE, 0)) * (1 + s.VAT_RATIO), 4, MidpointRounding.AwayFromZero) - Math.Round(s.HEIN_PRICE.Value, 4, MidpointRounding.AwayFromZero);
            }
        }
    }
}
