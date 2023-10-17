using HIS.Desktop.LocalStorage.ConfigApplication;
using HIS.Desktop.LocalStorage.LocalData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BillTransferAccounting.Config
{
    class AppConfig
    {
        private const string HFS_KEY__PAY_FORM_CODE = "HFS_KEY__PAY_FORM_CODE";

        private static string HisPayFormCode__Default;
        public static string HIS_PAY_FORM_CODE__DEFAULT
        {
            get
            {
                if (String.IsNullOrEmpty(HisPayFormCode__Default))
                {
                    HisPayFormCode__Default = String.IsNullOrEmpty(ConfigApplicationWorker.Get<string>(HFS_KEY__PAY_FORM_CODE)) ? GlobalVariables.HIS_PAY_FORM_CODE__CONSTANT : ConfigApplicationWorker.Get<string>(HFS_KEY__PAY_FORM_CODE);
                }
                return HisPayFormCode__Default;
            }
        }
    }
}
