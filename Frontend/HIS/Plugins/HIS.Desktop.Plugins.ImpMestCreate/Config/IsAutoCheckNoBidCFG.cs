using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Config
{
    class IsAutoCheckNoBidCFG
    {
        private const string isCheckNoBid = "HIS.Desktop.Plugins.ImpMestCreate.IsAutoCheckNoBidInCaseOfBusinessStock";

        internal static bool IsAutoCheckNoBid
        {
            get { return Inventec.Common.TypeConvert.Parse.ToInt64(HisConfigs.Get<string>(isCheckNoBid)) == 1; }
        }
    }
}
