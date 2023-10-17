using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintAggrExpMest
{
    class HisConfigKey
    {
        private const string CONFIG_KEY__NOT_SHOW_EXPEND = "HIS.Desktop.Plugins.Library.Print.NotShowExpend.262";

        internal static string NOT_SHOW_EXPEND
        {
            get
            {
                return HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(CONFIG_KEY__NOT_SHOW_EXPEND);
            }
        }
    }
}
