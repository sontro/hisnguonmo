using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineSaleBill
{
    class Config
    {
        private const string mps = "HIS.Desktop.Plugins.MedicineSaleBill.PrintNow";

        internal static string PrintNowMps
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(mps);
            }
        }

        private const string CtrlKey = "HIS.Desktop.Plugins.MedicineSaleBill.IsUsingFunctionKeyInsteadOfCtrlKey";

        internal static bool IsUsingFunctionKey
        {
            get
            {
                return (HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CtrlKey) == "1");
            }
        }

    }
}
