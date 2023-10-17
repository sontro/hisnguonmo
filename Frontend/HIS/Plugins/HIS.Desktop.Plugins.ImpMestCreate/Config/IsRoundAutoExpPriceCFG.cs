using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Config
{
    class IsRoundAutoExpPriceCFG
    {
        private const string warningDate = "HIS.Desktop.Plugins.ImpMestCreate.IsAutoRoundExpPrice";

        internal static bool IsRoundAutoExpPrice
        {
            get { return Inventec.Common.TypeConvert.Parse.ToInt64(HisConfigs.Get<string>(warningDate)) == 1; }
        }
    }
}
