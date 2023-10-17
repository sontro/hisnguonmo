using HIS.Desktop.LocalStorage.ConfigApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrExpMestDetail
{
    public static class AppConfigKeys
    {
        public const string CONFIG_KEY__CHE_DO_PHIEU_LINH_THUOC_GAY_NGHIEN_HUONG_TAM_THAN = "CONFIG_KEY__CHE_DO_PHIEU_LINH_THUOC_GAY_NGHIEN_HUONG_TAM_THAN";
        internal const string CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN = "CONFIG_KEY__HIS_DESKTOP__IN_GOP_GAY_NGHIEN_HUONG_THAN";

        private const string CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN = "CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN";
        internal const string CONFIG_KEY__IS_REASON_REQUIRED = "MOS.EXP_MEST.IS_REASON_REQUIRED";
        internal static string CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN
        {
            get
            {
                return ConfigApplicationWorker.Get<string>(CONFIG_KEY__CHE_DO_IN_CONG_KHAI_THUOC_BENH_NHAN);
            }
        }
    }
}
