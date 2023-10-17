using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class HisPatientInfoForChildCFG
    {
        private const string MOS__HIS_PATIENT__MUST_HAVE_NCS_INFO_FOR_CHILD = "MOS.HIS_PATIENT.MUST_HAVE_NCS_INFO_FOR_CHILD";
        private const string IsNotInfo = "0";

        private static bool? mustHaveNCSInfoForChild;
        public static bool MustHaveNCSInfoForChild
        {
            get
            {
                if (!mustHaveNCSInfoForChild.HasValue)
                {
                    mustHaveNCSInfoForChild = Get(SdaConfigs.Get<string>(MOS__HIS_PATIENT__MUST_HAVE_NCS_INFO_FOR_CHILD));
                }
                return mustHaveNCSInfoForChild.Value;
            }
        }

        static bool Get(string code)
        {
            bool result = true;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = !(code == IsNotInfo);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = true;
            }
            return result;
        }
    }
}
