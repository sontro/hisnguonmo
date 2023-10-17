using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InsuranceExpertise.Config
{
    class AutoLockAfterApprovalBHYTCFG
    {
        private const string CONFIG_KEY = "MOS.HIS_TREATMENT.AUTO_LOCK_AFTER_HEIN_APPROVAL";

        private const string IS_AUTO = "1";

        private static bool? isAutoLockAfterApproval;
        public static bool IsAutoLockAfterApprovalBHYT
        {
            get
            {
                if (!isAutoLockAfterApproval.HasValue)
                {
                    isAutoLockAfterApproval = Get(HisConfigCFG.GetValue(CONFIG_KEY));
                    
                }
                return isAutoLockAfterApproval.Value;
            }
        }

        static bool Get(string code)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = (code == IS_AUTO);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
