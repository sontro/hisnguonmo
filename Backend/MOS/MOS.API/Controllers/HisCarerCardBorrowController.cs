using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCarerCardBorrow;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisCarerCardBorrowController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCarerCardBorrowFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCarerCardBorrowFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARER_CARD_BORROW>> result = new ApiResultObject<List<HIS_CARER_CARD_BORROW>>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_CARER_CARD_BORROW> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_CARER_CARD_BORROW> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisCarerCardBorrowDeleteSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.DeleteSDO(param.ApiData);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
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
		
		[HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_CARER_CARD_BORROW> result = null;
            if (param != null && param.ApiData != null)
            {
                HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Borrow")]
        public ApiResult Borrow(ApiParam<HisCarerCardBorrowSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.Borrow(param.ApiData);
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
        [ActionName("Lost")]
        public ApiResult Lost(ApiParam<HisCarerCardBorrowLostSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.Lost(param.ApiData);
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
        [ActionName("UnLost")]
        public ApiResult UnLost(ApiParam<HisCarerCardBorrowUnLostSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.UnLost(param.ApiData);
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
        [ActionName("GiveBack")]
        public ApiResult Lost(ApiParam<HisCarerCardBorrowGiveBackSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.GiveBack(param.ApiData);
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
        [ActionName("UnGiveBack")]
        public ApiResult UnLost(ApiParam<HisCarerCardBorrowUnGiveBackSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CARER_CARD_BORROW> result = new ApiResultObject<HIS_CARER_CARD_BORROW>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
                    result = mng.UnGiveBack(param.ApiData);
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
