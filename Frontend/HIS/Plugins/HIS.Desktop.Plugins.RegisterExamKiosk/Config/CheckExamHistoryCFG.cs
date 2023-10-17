using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk
{
    class CheckExamHistoryCFG
    {
        private const string CONFIG_KEY = "HIS.Desktop.Plugins.Register.IS_CHECK_EXAM_HISTORY_TODAY";
        private const string IS_CHECK = "1";


        private static bool? isCheckExamHistory;
        public static bool IsCheckExamHistory
        {
            get
            {
                if (!isCheckExamHistory.HasValue)
                {
                    isCheckExamHistory = GetIsCheck(HisConfigs.Get<string>(CONFIG_KEY));
                }
                return isCheckExamHistory.Value;
            }
        }


        private static bool GetIsCheck(string value)
        {
            return (value == IS_CHECK);
        }
    }
}
