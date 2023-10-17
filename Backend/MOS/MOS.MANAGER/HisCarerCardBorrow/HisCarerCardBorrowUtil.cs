using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisCarerCardBorrow
{
    class HisCarerCardBorrowUtil : BusinessBase
    {
        internal static void ChangeAmountWithBorrowTime(HIS_SERE_SERV s, long borrowTime)
        {
            s.AMOUNT_TEMP = null;

            DateTime nowDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.Now().Value).Value.Date;
            DateTime borrowDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(borrowTime).Value.Date;

            long nowHour = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(Inventec.Common.DateTime.Get.Now().Value).Value.Hour;
            if (nowDate == borrowDate)
            {
                s.AMOUNT = 1;
            }
            else if (nowDate > borrowDate)
            {
                if (nowHour >= 12)
                {
                    s.AMOUNT = Inventec.Common.DateTime.Calculation.DifferenceDate(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(borrowDate).Value, Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(nowDate).Value) + 1;
                }
                else
                {
                    s.AMOUNT = Inventec.Common.DateTime.Calculation.DifferenceDate(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(borrowDate).Value, Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(nowDate).Value);
                }
            }
        }

        internal static void ChangeAmountWithBorrowAndGiveBackTime(HIS_SERE_SERV s, long borrowTime, long giveBackTime)
        {
            s.AMOUNT_TEMP = null;

            DateTime giveBackDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(giveBackTime).Value.Date;
            DateTime borrowDate = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(borrowTime).Value.Date;

            long giveBackHour = Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(giveBackTime).Value.Hour;
            if (giveBackDate == borrowDate)
            {
                s.AMOUNT = 1;
            }
            else if (giveBackDate > borrowDate)
            {
                if (giveBackHour >= 12)
                {
                    s.AMOUNT = Inventec.Common.DateTime.Calculation.DifferenceDate(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(borrowDate).Value, Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(giveBackDate).Value) + 1;
                }
                else
                {
                    s.AMOUNT = Inventec.Common.DateTime.Calculation.DifferenceDate(Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(borrowDate).Value, Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(giveBackDate).Value);
                }
            }
        }
    }
}
