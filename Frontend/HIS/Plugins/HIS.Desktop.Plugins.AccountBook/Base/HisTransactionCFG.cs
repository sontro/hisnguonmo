using Inventec.Common.Logging;
using Inventec.Core;
using MOS.Filter;
using System;
using System.Linq;
using Inventec.Common.Adapter;
using System.Collections.Generic;
using HIS.Desktop.ApiConsumer;
using Inventec.Common.LocalStorage.SdaConfig;

namespace HIS.Desktop.Plugins.HisAccountBookList.Base
{
    public class HisTransactionCFG
    {
        private static long transactionTypeCodeBill;
        public static long TRANSACTION_TYPE_ID__BILL
        {
            get
            {
                if (transactionTypeCodeBill == 0)
                {
                    transactionTypeCodeBill = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_TRANSACTION_TYPE__TRANSACTION_TYPE_CODE__BILL);
                }
                return transactionTypeCodeBill;
            }
            set
            {
                transactionTypeCodeBill = value;
            }
        }

        private static long transactionTypeIdDeposit;
        public static long TRANSACTION_TYPE_ID__DEPOSIT
        {
            get
            {
                if (transactionTypeIdDeposit == 0)
                {
                    transactionTypeIdDeposit = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_TRANSACTION_TYPE__TRANSACTION_TYPE_CODE__DEPOSIT);
                }
                return transactionTypeIdDeposit;
            }
            set
            {
                transactionTypeIdDeposit = value;
            }
        }

        private static long transactionTypeIdRepay;
        public static long TRANSACTION_TYPE_ID__REPAY
        {
            get
            {
                if (transactionTypeIdRepay == 0)
                {
                    transactionTypeIdRepay = GetId(ConfigKeys.DBCODE__HIS_RS__HIS_TRANSACTION_TYPE__TRANSACTION_TYPE_CODE__REPAY);
                }
                return transactionTypeIdRepay;
            }
            set
            {
                transactionTypeIdRepay = value;
            }
        }

        private static long GetId(string code)
        {
            long result = 0;
            try
            {
                SDA.EFMODEL.DataModels.SDA_CONFIG config = ConfigLoader.dictionaryConfig[code];
                if (config == null) throw new ArgumentNullException(code);
                string value = String.IsNullOrEmpty(config.VALUE) ? (String.IsNullOrEmpty(config.DEFAULT_VALUE) ? "" : config.DEFAULT_VALUE) : config.VALUE;
                if (String.IsNullOrEmpty(value)) throw new ArgumentNullException(code);
                HisTransactionTypeFilter filter = new HisTransactionTypeFilter();
                //sua lai
                filter.TRANSACTION_TYPE_CODE = value;
                var apiresult = new BackendAdapter(new CommonParam()).Get<List<MOS.EFMODEL.DataModels.HIS_TRANSACTION_TYPE>>(HisRequestUriStore.HIS_TRANSACTION_TYPE_GET, ApiConsumers.MosConsumer, filter, HIS.Desktop.Controls.Session.SessionManager.ActionLostToken, new CommonParam());
                var data = apiresult.FirstOrDefault();
                if (!(data != null && data.ID > 0)) throw new ArgumentNullException(code);
                result = data.ID;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }
    }
}
