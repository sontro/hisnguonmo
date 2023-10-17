using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class HisPrintTestCFG
    {

        const string isSplit = "1";
        private static bool? assignPrintTEST;
        public static bool AssignPrintTEST
        {
            get
            {
                try
                {
                    if (!assignPrintTEST.HasValue)
                    {
                        assignPrintTEST = GetData(SdaConfigs.Get<string>(ExtensionConfigKey.HIS_Desktop_Plugins_AssignServicePrintTEST));
                    }
                }
                catch (Exception ex)
                {
                    assignPrintTEST = false;
                    Inventec.Common.Logging.LogSystem.Error(ex);
                }
                
                return assignPrintTEST.Value;
            }
            set
            {
                assignPrintTEST = value;
            }
        }

        private static bool GetData(string code)
        {
            bool result = false;
            try
            {
                result = (isSplit == code);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
