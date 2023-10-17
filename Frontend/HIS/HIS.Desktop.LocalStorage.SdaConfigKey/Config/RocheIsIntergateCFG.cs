using Inventec.Common.LocalStorage.SdaConfig;
using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class RocheIsIntergateCFG
    {
        private const string MOS_ROCHE_IS_INTEGRATE = "MOS.ROCHE_IS_INTEGRATE";

        private static long genderIdIntergrate;
        public static long ROCHE_IS_INTEGRATE
        {
            get
            {
                if (genderIdIntergrate == 0)
                {
                    genderIdIntergrate = GetId(SdaConfigs.Get<string>(MOS_ROCHE_IS_INTEGRATE));
                }
                return genderIdIntergrate;
            }
            set
            {
                genderIdIntergrate = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                result = Inventec.Common.TypeConvert.Parse.ToInt64(code);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
