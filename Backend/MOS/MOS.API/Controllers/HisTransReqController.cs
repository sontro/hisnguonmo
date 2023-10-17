using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOS.API.Controllers
{
    public partial class HisTransReqController : BaseApiController
    {
        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransReqFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTransReqFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANS_REQ>> result = new ApiResultObject<List<HIS_TRANS_REQ>>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_TRANS_REQ> param)
        {
            try
            {
                ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_TRANS_REQ> param)
        {
            try
            {
                ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_TRANS_REQ> result = null;
            if (param != null && param.ApiData != null)
            {
                HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("CreateBillTwoBook")]
        public ApiResult CreateBillTwoBook(ApiParam<HisTransReqBillTwoBookSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANS_REQ>> result = new ApiResultObject<List<HIS_TRANS_REQ>>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("CallbackPaylater")]
        public ApiResult CallbackPaylater(ApiParam<HisTransReqCallbackSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                    result = mng.CallbackPaylater(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("CreateBill")]
        public ApiResult CreateBill(ApiParam<HisTransReqBillSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("UpdateBankInfo")]
        public ApiResult UpdateQrInfo(ApiParam<HisTransReqBankInfoSDO> param)
        {
            try
            {
                ApiResultObject<HIS_TRANS_REQ> result = new ApiResultObject<HIS_TRANS_REQ>(null);
                if (param != null)
                {
                    HisTransReqManager mng = new HisTransReqManager(param.CommonParam);
                    result = mng.UpdateQrInfo(param.ApiData);
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
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<MerchantPaymentResultSDO>))]
        [ActionName("BankPayment")]
        public ApiResult BankPayment(MerchantPaymentSDO param)
        {
            try
            {
                MerchantPaymentResultSDO result = new MerchantPaymentResultSDO();
                if (param != null)
                {
                    CommonParam CommonParam = new CommonParam();
                    HisTransReqManager mng = new HisTransReqManager(CommonParam);
                    ApiResultObject<MerchantPaymentResultSDO> resultData = mng.BankPayment(param);
                    if (resultData != null && resultData.Data != null)
                    {
                        result = resultData.Data;
                    }
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
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<PaymentVietinbankResultTDO>))]
        [ActionName("QrPaymentVietinbank")]
        public ApiResult QrPaymentVietinbank(PaymentVietinbankTDO param)
        {
            try
            {
                PaymentVietinbankResultTDO result = new PaymentVietinbankResultTDO();
                if (param != null)
                {
                    CommonParam CommonParam = new CommonParam();
                    HisTransReqManager mng = new HisTransReqManager(CommonParam);
                    ApiResultObject<PaymentVietinbankResultTDO> resultData = mng.QrPaymentVietinbank(param);
                    if (resultData != null && resultData.Data != null)
                    {
                        result = resultData.Data;
                    }
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
        [AllowAnonymous]
        [ResponseType(typeof(ApiResultObject<PaymentBidvResultTDO>))]
        [ActionName("QrPaymentBIDV")]
        public IHttpActionResult QrPaymentBIDV(PaymentBidvTDO param, [FromUri] long id)
        {
            try
            {
                if (id != 01043)
                {
                    return new ApiForbiddenResult(this.ActionContext.Request);
                }
                PaymentBidvResultTDO result = new PaymentBidvResultTDO();
                if (param != null)
                {
                    CommonParam CommonParam = new CommonParam();
                    HisTransReqManager mng = new HisTransReqManager(CommonParam);
                    ApiResultObject<PaymentBidvResultTDO> resultData = mng.QrPaymentBIDV(param);
                    if (resultData != null && resultData.Data != null)
                    {
                        result = resultData.Data;
                    }
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
