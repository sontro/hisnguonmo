using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InveImpMestEdit.Config
{
    class HisImpMestTypeAuthorziedCFG
    {
        private const string CONFIG_KEY__IMP_MEST_TYPE__AUTHORIZED = "MOS.HIS_IMP_MEST.HIS_IMP_MEST_TYPE.AUTHORIZED";
        private const string IsAuthorized = "1";

        private static bool? impMestType_IsAuthorized;
        public static bool ImpMestType_IsAuthorized
        {
            get
            {
                if (!impMestType_IsAuthorized.HasValue)
                {
                    impMestType_IsAuthorized = Get(SdaConfigs.Get<string>(CONFIG_KEY__IMP_MEST_TYPE__AUTHORIZED));
                }
                return impMestType_IsAuthorized.Value;
            }
        }

        static bool Get(string code)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = (code == IsAuthorized);
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
