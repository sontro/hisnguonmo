using HIS.Desktop.LocalStorage.ConfigApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.PrintAggrExpMest
{
    class AppConfigKeys
    {
        private const string CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN = "CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN";

        internal static string CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN
        {
            get
            {
                return ConfigApplicationWorker.Get<string>(CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN);
            }
        }
    }
}
