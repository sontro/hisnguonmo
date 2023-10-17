using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.MANAGER.HisTransaction.Bill.BillTwoBook;
using MOS.MANAGER.HisTransaction.Cancel;
using MOS.MANAGER.HisTransaction.Deposit;
using MOS.MANAGER.HisTransaction.Repay;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    public partial class HisTransactionManager : BusinessBase
    {

        [Logger]
        public ApiResultObject<bool> CheckDeposit(HisTransactionDepositSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTransactionDepositCheck(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CheckRepay(HisTransactionRepaySDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTransactionRepayCheck(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CheckBill(HisTransactionBillSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    V_HIS_ACCOUNT_BOOK accountBook = null;
                    HIS_TREATMENT treatment = null;
                    List<HIS_SERE_SERV> sereServs = null;
                    decimal exemption = 0;
                    decimal fundPaidTotal = 0;
                    decimal? transferAmount = null;
                    HIS_TRANSACTION originalTransaction = null;
                    WorkPlaceSDO workplace = null;
                    resultData = new HisTransactionBillCheck(param).Run(data, false, ref workplace, ref accountBook, ref treatment, ref sereServs, ref originalTransaction, ref exemption, ref fundPaidTotal, ref transferAmount);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CheckCancel(HisTransactionCancelSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTransactionCancelCheck(param).Run(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> CheckBillTwoBook(HisTransactionBillTwoBookSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    V_HIS_ACCOUNT_BOOK invoiceAccountBook = null;
                    V_HIS_ACCOUNT_BOOK recieptAccountBook = null;
                    HIS_TREATMENT treatment = null;
                    List<HIS_SERE_SERV> sereServs = null;
                    List<HIS_SERE_SERV_BILL> sereServBills = null;
                    WorkPlaceSDO workplace = null;
                    resultData = new HisTransactionBillTwoBookCheck(param).Run(data, false, ref workplace, ref recieptAccountBook, ref invoiceAccountBook, ref treatment, ref sereServs, ref sereServBills);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

    }
}
