using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class TreatmentEndTypeDefaultCFG
    {
        private const string TreatmentEndTypeDefault = "HIS.Desktop.Plugins.TreatmentFinish.TreatmentEndTypeDefault";

        private static long treatmentEndTypeDefault;
        public static long TreatmentEndTypeDefaultID
        {
            get
            {
                if (treatmentEndTypeDefault <= 0)
                {
                    treatmentEndTypeDefault = GetID(TreatmentEndTypeDefault);
                }
                return treatmentEndTypeDefault;
            }
            set
            {
                treatmentEndTypeDefault = value;
            }
        }

        private static long GetID(string code)
        {
            long result = 0;
            try
            {
                result = Inventec.Common.TypeConvert.Parse.ToInt64(SdaConfigs.Get<String>(code));
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
