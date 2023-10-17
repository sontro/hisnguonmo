using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class RegisterVisibleButtonBillCFG
    {
        private const string SERVICE_REQUEST_REGISTER__IS_PRINT_AFTER_SAVE__KEY = "EXE.SERVICE_REQUEST_REGISTER.IS_PRINT_AFTER_SAVE";
        private const string SERVICE_REQUEST_REGISTER__IS_VISIBLE_BILL__KEY = "EXE.SERVICE_REQUEST_REGISTER.IS_VISIBLE_BILL";

        private static string serviceRequestRegisterIsPrintAfterSave;
        public static string SERVICE_REQUEST_REGISTER__IS_PRINT_AFTER_SAVE
        {
            get
            {
                if (serviceRequestRegisterIsPrintAfterSave == null)
                {
                    serviceRequestRegisterIsPrintAfterSave = GetValue(SERVICE_REQUEST_REGISTER__IS_PRINT_AFTER_SAVE__KEY);
                }
                return serviceRequestRegisterIsPrintAfterSave;
            }
            set
            {
                serviceRequestRegisterIsPrintAfterSave = value;
            }
        }

        private static string serviceRequestRegisterIsVisibleBill;
        public static string SERVICE_REQUEST_REGISTER__IS_VISIBLE_BILL
        {
            get
            {
                if (serviceRequestRegisterIsVisibleBill == null)
                {
                    serviceRequestRegisterIsVisibleBill = GetValue(SERVICE_REQUEST_REGISTER__IS_VISIBLE_BILL__KEY);
                }
                return serviceRequestRegisterIsVisibleBill;
            }
            set
            {
                serviceRequestRegisterIsVisibleBill = value;
            }
        }

        private static string GetValue(string code)
        {
            string result = null;
            try
            {
                SDA.EFMODEL.DataModels.SDA_CONFIG config = Inventec.Common.LocalStorage.SdaConfig.ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                result = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(result)) throw new ArgumentNullException(code);
                return result;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

    }
}
