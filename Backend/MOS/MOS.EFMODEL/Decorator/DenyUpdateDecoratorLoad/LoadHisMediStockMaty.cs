using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DenyUpdateDecorator
    {
        private static void LoadHisMediStockMaty()
        {
            List<string> pies = new List<string>();
            pies.Add("CREATOR");
            pies.Add("APP_CREATOR");
            pies.Add("CREATE_TIME");
            //pies.Add("REAL_BASE_AMOUNT");//ko cho phep cap nhat truong nay (chi duoc thuc hien qua sql)

            properties[typeof(HIS_MEDI_STOCK_MATY)] = pies;
        }
    }
}
