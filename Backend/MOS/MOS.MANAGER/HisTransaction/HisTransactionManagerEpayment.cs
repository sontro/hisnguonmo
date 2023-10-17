using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.MANAGER.HisTransaction.Bill.BillTwoBook;
using MOS.MANAGER.HisTransaction.Cancel;
using MOS.MANAGER.HisTransaction.Deposit;
using MOS.MANAGER.HisTransaction.Epayment.Deposit;
using MOS.MANAGER.HisTransaction.Epayment.Bill;
using MOS.MANAGER.HisTransaction.Repay;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransaction
{
    public partial class HisTransactionManager : BusinessBase
    {

        [Logger]
        public ApiResultObject<EpaymentDepositResultSDO> EpaymentDeposit(EpaymentDepositSD data)
        {
            ApiResultObject<EpaymentDepositResultSDO> result = new ApiResultObject<EpaymentDepositResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                EpaymentDepositResultSDO resultData = null;
                if (valid)
                {
                    new EpaymentDeposit(param).Run(data, ref resultData);
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
        public ApiResultObject<EpaymentBillResultSDO> EpaymentBill(EpaymentBillSDO data)
        {
            ApiResultObject<EpaymentBillResultSDO> result = new ApiResultObject<EpaymentBillResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                EpaymentBillResultSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new EpaymentBill(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
