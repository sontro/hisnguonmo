using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.LocalStorage.SdaConfig;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExamServiceReqExecute.Config
{
    public class HisTransactionTypeCFG
    {
        private static long hisTransactionTypeId__Bill;
        public static long HisTransactionTypeId__Bill
        {
            get
            {
                if (hisTransactionTypeId__Bill <= 0)
                {
                    hisTransactionTypeId__Bill = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("DBCODE.HIS_RS.HIS_TRANSACTION_TYPE.TRANSACTION_TYPE_CODE.BILL"));
                }
                return hisTransactionTypeId__Bill;
            }
        }

        private static long hisTransactionTypeId__Deposit;
        public static long HisTransactionTypeId__Deposit
        {
            get
            {
                if (hisTransactionTypeId__Deposit <= 0)
                {
                    hisTransactionTypeId__Deposit = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("DBCODE.HIS_RS.HIS_TRANSACTION_TYPE.TRANSACTION_TYPE_CODE.DEPOSIT"));
                }
                return hisTransactionTypeId__Deposit;
            }
        }

        private static long hisTransactionTypeId__Repay;
        public static long HisTransactionTypeId__Repay
        {
            get
            {
                if (hisTransactionTypeId__Repay <= 0)
                {
                    hisTransactionTypeId__Repay = GetId(HIS.Desktop.LocalStorage.HisConfig.HisConfigs.Get<string>("DBCODE.HIS_RS.HIS_TRANSACTION_TYPE.TRANSACTION_TYPE_CODE.REPAY"));
                }
                return hisTransactionTypeId__Repay;
            }
        }


        private static long GetId(string transactionTypeCode)
        {
            long result = 0;
            try
            {
                if (String.IsNullOrEmpty(transactionTypeCode)) throw new NullReferenceException("Code");
                var transactionType = BackendDataWorker.Get<HIS_TRANSACTION_TYPE>().FirstOrDefault(o => o.TRANSACTION_TYPE_CODE == transactionTypeCode);
                if (transactionType != null)
                {
                    result = transactionType.ID;
                }
                if (result == 0) throw new NullReferenceException(transactionTypeCode);
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
