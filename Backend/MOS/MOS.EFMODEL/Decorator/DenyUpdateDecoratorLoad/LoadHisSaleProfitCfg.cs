using MOS.EFMODEL.DataModels;
using System.Collections.Generic;
using System.Reflection;

namespace MOS.EFMODEL.Decorator
{
    public partial class DenyUpdateDecorator
    {
        private static void LoadHisSaleProfitCfg()
        {
            List<string> pies = new List<string>();
            pies.Add("CREATOR");
            pies.Add("APP_CREATOR");
            pies.Add("CREATE_TIME");

            properties[typeof(HIS_SALE_PROFIT_CFG)] = pies;
        }
    }
}
