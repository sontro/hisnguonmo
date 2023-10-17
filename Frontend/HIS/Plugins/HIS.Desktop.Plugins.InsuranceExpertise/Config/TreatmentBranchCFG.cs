using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InsuranceExpertise.Config
{
    class TreatmentBranchCFG
    {
        private const string ConfigKey = "HIS.Desktop.Plugins.Treatment.Is_Show_All_Branch";
        private const string IsShow = "1";

        private static bool? isShowAllBranch;
        public static bool IsShowAllBranch
        {
            get
            {
                if (!isShowAllBranch.HasValue)
                {
                    isShowAllBranch = Get(HisConfigCFG.GetValue(ConfigKey));
                }
                return isShowAllBranch.Value;
            }
        }

        static bool Get(string code)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = (code == IsShow);
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
