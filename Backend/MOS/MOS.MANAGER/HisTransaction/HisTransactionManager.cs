using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.Base;
using MOS.MANAGER.HisTransaction.Bill;
using MOS.MANAGER.HisTransaction.Bill.BillByDeposit;
using MOS.MANAGER.HisTransaction.Bill.BillOther;
using MOS.MANAGER.HisTransaction.Bill.BillTwoBook;
using MOS.MANAGER.HisTransaction.Bill.DebtCollection;
using MOS.MANAGER.HisTransaction.Bill.SaleExpMest;
using MOS.MANAGER.HisTransaction.Bill.Vaccin;
using MOS.MANAGER.HisTransaction.Cancel;
using MOS.MANAGER.HisTransaction.Debt.Create;
using MOS.MANAGER.HisTransaction.Debt.DrugStoreCreate;
using MOS.MANAGER.HisTransaction.Deposit;
using MOS.MANAGER.HisTransaction.Repay;
using MOS.MANAGER.HisTransaction.Uncancel;
using MOS.MANAGER.HisTransaction.Update;
using MOS.MANAGER.HisTransaction.CancelInvoice;
using MOS.SDO;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisTransaction.UnrejectCancellationRequest;
using MOS.MANAGER.HisTransaction.RejectCancellationRequest;
using MOS.MANAGER.HisTransaction.RequestCancel;
using MOS.MANAGER.HisTransaction.DeleteCancellationRequest;

namespace MOS.MANAGER.HisTransaction
{
    public partial class HisTransactionManager : BusinessBase
    {
        public HisTransactionManager()
            : base()
        {

        }

        public HisTransactionManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_TRANSACTION>> Get(HisTransactionFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANSACTION>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANSACTION> resultData = new List<HIS_TRANSACTION>();
                if (valid)
                {
                    resultData = new HisTransactionGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_TRANSACTION>> GetView(HisTransactionViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_TRANSACTION>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_TRANSACTION> resultData = new List<V_HIS_TRANSACTION>();
                if (valid)
                {
                    resultData = new HisTransactionGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_TRANSACTION> Update(HIS_TRANSACTION data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = new HIS_TRANSACTION();
                if (valid && new HisTransactionUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_TRANSACTION> DepositLock(long id)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDepositLock(param).DepositLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> DepositUnlock(TransactionLockSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDepositLock(param).DepositUnlock(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> CreateDeposit(HisTransactionDepositSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDepositCreate(param).CreateDeposit(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> DebtCollect(HisTransactionDebtCollecSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDebtCollect(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> CreateRepay(HisTransactionRepaySDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionRepayCreate(param).CreateRepay(data, ref resultData);
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
        public ApiResultObject<HisTransactionBillResultSDO> CreateBill(HisTransactionBillSDO data)
        {
            ApiResultObject<HisTransactionBillResultSDO> result = new ApiResultObject<HisTransactionBillResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(data);
                HisTransactionBillResultSDO resultData = null;
                if (valid)
                {
                    if (param == null)
                    {
                        param = new CommonParam();
                    }
                    new HisTransactionBillCreate(param).CreateBill(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> UpdateFile(HIS_TRANSACTION data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionUpdate(param).UpdateFile(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> Cancel(HisTransactionCancelSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionCancel(param).Cancel(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> Uncancel(HisTransactionUncancelSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionUncancel(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> UpdateInfo(HisTransactionUpdateInfoSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionUpdateInfo(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> CreateBillWithBillGood(HisTransactionBillGoodsSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionBillCreateWithBillGoods(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_TRANSACTION>> UpdateNationalCode(List<HIS_TRANSACTION> listData)
        {
            ApiResultObject<List<HIS_TRANSACTION>> result = new ApiResultObject<List<HIS_TRANSACTION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_TRANSACTION> resultData = null;
                if (valid)
                {
                    new HisTransactionUpdateNationalCode(param).Run(listData, ref resultData);
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
        public ApiResultObject<List<HIS_TRANSACTION>> CancelNationalCode(List<long> listData)
        {
            ApiResultObject<List<HIS_TRANSACTION>> result = new ApiResultObject<List<HIS_TRANSACTION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_TRANSACTION> resultData = null;
                if (valid)
                {
                    new HisTransactionCancelNationalCode(param).Run(listData, ref resultData);
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
        public ApiResultObject<List<V_HIS_TRANSACTION>> CreateListBill(List<HisTransactionBillSDO> listData)
        {
            ApiResultObject<List<V_HIS_TRANSACTION>> result = new ApiResultObject<List<V_HIS_TRANSACTION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<V_HIS_TRANSACTION> resultData = null;
                if (valid)
                {
                    new HisTransactionBillCreateList(param).Run(listData, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> CreateOtherBill(HisTransactionOtherBillSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionOtherBillCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> UpdateNumOrder(HIS_TRANSACTION data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionUpdateNumOrder(param).Run(data, ref resultData);
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
        public ApiResultObject<List<V_HIS_TRANSACTION>> CreateBillTwoBook(HisTransactionBillTwoBookSDO data)
        {
            ApiResultObject<List<V_HIS_TRANSACTION>> result = new ApiResultObject<List<V_HIS_TRANSACTION>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<V_HIS_TRANSACTION> resultData = null;
                if (valid)
                {
                    new HisTransactionBillTwoBookCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> CreateBillVaccin(HisTransactionBillVaccinSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionBillVaccinCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<HisTransactionTotalPriceSDO> GetTotalPriceSdo(HisTransactionViewFilterQuery filter)
        {
            ApiResultObject<HisTransactionTotalPriceSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisTransactionTotalPriceSDO resultData = null;
                if (valid)
                {
                    resultData = new HisTransactionGet(param).GetTotalPriceSdo(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<V_HIS_TRANSACTION> CreateDebt(HisTransactionDebtSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDebtCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<HisDrugStoreDebtResultSDO> CreateDrugStoreDebt(HisTransactionDrugStoreDebtSDO data)
        {
            ApiResultObject<HisDrugStoreDebtResultSDO> result = new ApiResultObject<HisDrugStoreDebtResultSDO>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDrugStoreDebtResultSDO resultData = null;
                if (valid)
                {
                    new HisTransactionDrugStoreDebtCreate(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> Delete(HisTransactionDeleteSDO data)
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
                    resultData = new Truncate.HisTransactionTruncate(param).Run(data);
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
        public ApiResultObject<HisTransactionGeneralInfoSDO> GetGeneralInfo(HisTransactionGeneralInfoFilter filter)
        {
            ApiResultObject<HisTransactionGeneralInfoSDO> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                HisTransactionGeneralInfoSDO resultData = null;
                if (valid)
                {
                    resultData = new HisTransactionGet(param).GetGeneralInfo(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateInvoiceInfo(HisTransactionInvoiceInfoSDO data)
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
                    resultData = new HisTransactionUpdateInvoiceInfo(param).Run(data);
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
        public ApiResultObject<bool> UpdateInvoiceListInfo(HisTransactionInvoiceListInfoSDO data)
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
                    resultData = new HisTransactionUpdateInvoiceInfo(param).Run(data);
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
        public ApiResultObject<List<HisTransactionBillResultSDO>> BillByDeposit(HisTransactionBillByDepositSDO data)
        {
            ApiResultObject<List<HisTransactionBillResultSDO>> result = new ApiResultObject<List<HisTransactionBillResultSDO>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HisTransactionBillResultSDO> resultData = null;
                if (valid)
                {
                    new HisTransactionBillByDeposit(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> CancelInvoice(CancelInvoiceSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransactionCancelInvoice(param).Run(data, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

        [Logger]
        public ApiResultObject<bool> UpdateEInvoiceUrl(HisTransactionInvoiceUrlSDO data)
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
                    resultData = new HisTransactionUpdateInvoiceUrl(param).Run(data);
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
        public ApiResultObject<V_HIS_TRANSACTION> UnrejectCancellationRequest(HisTransactionUnrejectCancellationRequestSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionUnrejectCancellationRequest(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> RejectCancellationRequest(HisTransactionRejectCancellationRequestSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionRejectCancellationRequest(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> RequestCancel(HisTransactionRequestCancelSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionRequestCancel(param).Run(data, ref resultData);
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
        public ApiResultObject<V_HIS_TRANSACTION> DeleteCancellationRequest(HisTransactionDeleteCancellationRequestSDO data)
        {
            ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                V_HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionDeleteCancellationRequest(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION> BillLock(long id)
        {
            ApiResultObject<HIS_TRANSACTION> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSACTION resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransactionLock(param).BillLock(id, ref resultData);
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

        [Logger]
        public ApiResultObject<HIS_TRANSACTION> BillUnlock(TransactionLockSDO data)
        {
            ApiResultObject<HIS_TRANSACTION> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION resultData = null;
                if (valid)
                {
                    new HisTransactionLock(param).BillUnlock(data, ref resultData);
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
