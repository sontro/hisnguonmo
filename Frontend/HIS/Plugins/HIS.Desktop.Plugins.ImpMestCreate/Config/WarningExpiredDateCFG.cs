using HIS.Desktop.LocalStorage.HisConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Config
{
    class WarningExpiredDateCFG
    {
        private const string warningDate = "HIS.Desktop.Plugins.ImpMestCreate.WarningExpiredDate";

        internal static long WarningExpiredDate
        {
            get { return Inventec.Common.TypeConvert.Parse.ToInt64(HisConfigs.Get<string>(warningDate)); }
        }
    }
}
