using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionList.Config
{
    public class TransactionCancelCFG
    {
        private const string TRANSACTION_CANCEL__ALLOW_OTHER_LOGINNAME = "HIS.HIS_TRANSACTION.TRANSACTION_CANCEL.ALLOW_OTHER_LOGINNAME";

        private const string Is_Allow = "1";

        private static bool? hisTransaction_Cancel__AllowOtherLoginname;
        public static bool Transaction_Cancel__AllowOtherLoginname
        {
            get
            {
                if (!hisTransaction_Cancel__AllowOtherLoginname.HasValue)
                {
                    hisTransaction_Cancel__AllowOtherLoginname = Get(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>(TRANSACTION_CANCEL__ALLOW_OTHER_LOGINNAME));
                }
                return hisTransaction_Cancel__AllowOtherLoginname.Value;
            }
        }
     
        static bool Get(string code)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = (code == Is_Allow);
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
