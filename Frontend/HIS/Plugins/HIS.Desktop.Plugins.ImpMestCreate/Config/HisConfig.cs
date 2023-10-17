using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Config
{
    class HisConfig
    {
        private const string EditSaleProfitCFG = "HIS.Desktop.Plugins.ImpMestCreate.EditSaleProfit";
        private const string AllowDuplicateCFG = "HIS.Desktop.Plugins.ImpMestCreate.AllowDuplicate";
        internal static bool DisableSaleProfitCFG
        {
            get { return Inventec.Common.TypeConvert.Parse.ToInt64(HisConfigs.Get<string>(EditSaleProfitCFG)) != 1; }
        }
        internal static bool AllowDuplicate
        {
            get { return Inventec.Common.TypeConvert.Parse.ToInt64(HisConfigs.Get<string>(AllowDuplicateCFG)) == 1; }
        }
    }
}
