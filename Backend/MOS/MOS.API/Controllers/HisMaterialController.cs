using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMaterial;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMaterialFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MATERIAL>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMaterialViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MATERIAL>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMaterialStockViewFilter>), "param")]
        [ActionName("GetInStockMaterial")]
        public ApiResult GetInStockMaterial(ApiParam<HisMaterialStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialInStockSDO>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                    result = mng.GetInStockMaterial(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterial2StockFilter>), "param")]
        [ActionName("GetIn2StockMaterial")]
        public ApiResult GetIn2StockMaterial(ApiParam<HisMaterial2StockFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialIn2StockSDO>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                    result = mng.GetIn2StockMaterial(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialStockViewFilter>), "param")]
        [ActionName("GetInStockMaterialWithTypeTree")]
        public ApiResult GetInStockMaterialWithTypeTree(ApiParam<HisMaterialStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMaterialInStockSDO>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                    result = mng.GetInStockMaterialWithTypeTreeOrderByAmount(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMaterialStockViewFilter>), "param")]
        [ActionName("GetInStockMaterialWithTypeTreeOrderByExpiredDate")]
        public ApiResult GetInStockMaterialWithTypeTreeOrderByExpiredDate(ApiParam<HisMaterialStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<List<HisMaterialInStockSDO>>> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                    result = mng.GetInStockMaterialWithTypeTreeOrderByExpiredDate(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_MATERIAL> param)
        {
            try
            {
                ApiResultObject<HIS_MATERIAL> result = null;
                if (param != null)
                {
                    HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<HisMaterialChangeLockSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Unlock")]
        public ApiResult Unlock(ApiParam<HisMaterialChangeLockSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                result = mng.Unlock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("ReturnAvailable")]
        public ApiResult ReturnAvailable(ApiParam<HisMaterialReturnAvailableSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMaterialManager mng = new HisMaterialManager(param.CommonParam);
                result = mng.ReturnAvailable(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
