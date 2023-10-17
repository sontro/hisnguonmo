using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisTransaction;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisTransactionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTransactionFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANSACTION>> result = null;
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisTransactionViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TRANSACTION>> result = null;
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateDeposit")]
        public ApiResult CreateDeposit(ApiParam<HisTransactionDepositSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateDeposit(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("DebtCollect")]
        public ApiResult DebtCollect(ApiParam<HisTransactionDebtCollecSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.DebtCollect(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateRepay")]
        public ApiResult CreateRepay(ApiParam<HisTransactionRepaySDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateRepay(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateBill")]
        public ApiResult CreateBill(ApiParam<HisTransactionBillSDO> param)
        {
            try
            {
                ApiResultObject<HisTransactionBillResultSDO> result = new ApiResultObject<HisTransactionBillResultSDO>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateBill(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateFile")] 
        public ApiResult UpdateFile(ApiParam<HIS_TRANSACTION> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateFile(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Cancel")]
        public ApiResult Cancel(ApiParam<HisTransactionCancelSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.Cancel(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Uncancel")]
        public ApiResult Uncancel(ApiParam<HisTransactionUncancelSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.Uncancel(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateInfo")]
        public ApiResult UpdateInfo(ApiParam<HisTransactionUpdateInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateInfo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("DepositLock")]
        public ApiResult DepositLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.DepositLock(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("DepositUnlock")]
        public ApiResult DepositUnlock(ApiParam<TransactionLockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.DepositUnlock(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateBillWithBillGood")]
        public ApiResult CreateBillWithBillGood(ApiParam<HisTransactionBillGoodsSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateBillWithBillGood(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateNationalCode")]
        public ApiResult UpdateNationalCode(ApiParam<List<HIS_TRANSACTION>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANSACTION>> result = new ApiResultObject<List<HIS_TRANSACTION>>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateNationalCode(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CancelNationalCode")]
        public ApiResult CancelNationalCode(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANSACTION>> result = new ApiResultObject<List<HIS_TRANSACTION>>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CancelNationalCode(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateBillTwoBook")]
        public ApiResult CreateBillTwoBook(ApiParam<HisTransactionBillTwoBookSDO> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TRANSACTION>> result = new ApiResultObject<List<V_HIS_TRANSACTION>>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateBillTwoBook(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateOtherBill")]
        public ApiResult CreateOtherBill(ApiParam<HisTransactionOtherBillSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateOtherBill(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateNumOrder")]
        public ApiResult UpdateNumOrder(ApiParam<HIS_TRANSACTION> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateNumOrder(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateBillVaccin")]
        public ApiResult CreateBillVaccin(ApiParam<HisTransactionBillVaccinSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateBillVaccin(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionViewFilterQuery>), "param")]
        [ActionName("GetTotalPriceSdo")]
        public ApiResult GetTotalPriceSdo(ApiParam<HisTransactionViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<HisTransactionTotalPriceSDO> result = null;
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.GetTotalPriceSdo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateDebt")]
        public ApiResult CreateDebt(ApiParam<HisTransactionDebtSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateDebt(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CreateDrugStoreDebt")]
        public ApiResult CreateDrugStoreDebt(ApiParam<HisTransactionDrugStoreDebtSDO> param)
        {
            try
            {
                ApiResultObject<HisDrugStoreDebtResultSDO> result = new ApiResultObject<HisDrugStoreDebtResultSDO>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CreateDrugStoreDebt(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisTransactionDeleteSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionGeneralInfoFilter>), "param")]
        [ActionName("GetGeneralInfo")]
        public ApiResult GetGeneralInfo(ApiParam<HisTransactionGeneralInfoFilter> param)
        {
            try
            {
                ApiResultObject<HisTransactionGeneralInfoSDO> result = null;
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.GetGeneralInfo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateInvoiceInfo")]
        public ApiResult UpdateInvoiceInfo(ApiParam<HisTransactionInvoiceInfoSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateInvoiceInfo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateInvoiceListInfo")]
        public ApiResult UpdateInvoiceListInfo(ApiParam<HisTransactionInvoiceListInfoSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateInvoiceListInfo(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BillByDeposit")]
        public ApiResult BillByDeposit(ApiParam<HisTransactionBillByDepositSDO> param)
        {
            try
            {
                ApiResultObject<List<HisTransactionBillResultSDO>> result = new ApiResultObject<List<HisTransactionBillResultSDO>>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.BillByDeposit(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("CancelInvoice")]
        public ApiResult CancelInvoice(ApiParam<CancelInvoiceSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.CancelInvoice(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateEInvoiceUrl")]
        public ApiResult UpdateEInvoiceUrl(ApiParam<HisTransactionInvoiceUrlSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UpdateEInvoiceUrl(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UnrejectCancellationRequest")]
        public ApiResult UnrejectCancellationRequest(ApiParam<HisTransactionUnrejectCancellationRequestSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.UnrejectCancellationRequest(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("RejectCancellationRequest")]
        public ApiResult RejectCancellationRequest(ApiParam<HisTransactionRejectCancellationRequestSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.RejectCancellationRequest(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("RequestCancel")]
        public ApiResult RequestCancel(ApiParam<HisTransactionRequestCancelSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.RequestCancel(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("DeleteCancellationRequest")]
        public ApiResult DeleteCancellationRequest(ApiParam<HisTransactionDeleteCancellationRequestSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_TRANSACTION> result = new ApiResultObject<V_HIS_TRANSACTION>(null);
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.DeleteCancellationRequest(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("BillLock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_TRANSACTION> result = null;
            if (param != null && param.ApiData != null)
            {
                HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                result = mng.BillLock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("BillUnlock")]
        public ApiResult BillUnlock(ApiParam<TransactionLockSDO> param)
        {
            ApiResultObject<HIS_TRANSACTION> result = new ApiResultObject<HIS_TRANSACTION>();
            if (param != null && param.ApiData != null)
            {
                HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                result = mng.BillUnlock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
