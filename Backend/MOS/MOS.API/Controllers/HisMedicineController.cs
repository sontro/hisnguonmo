using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMedicineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineStockViewFilter>), "param")]
        [ActionName("GetInStockMedicine")]
        public ApiResult GetInStockMedicine(ApiParam<HisMedicineStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineInStockSDO>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                    result = mng.GetInStockMedicine(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicine2StockFilter>), "param")]
        [ActionName("GetIn2StockMedicine")]
        public ApiResult GetIn2StockMedicine(ApiParam<HisMedicine2StockFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineIn2StockSDO>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                    result = mng.GetIn2StockMedicine(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineStockViewFilter>), "param")]
        [ActionName("GetInStockMedicineWithTypeTree")]
        public ApiResult GetInStockMedicineWithTypeTree(ApiParam<HisMedicineStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMedicineInStockSDO>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                    result = mng.GetInStockMedicineWithTypeTreeOrderByAmount(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisMedicineStockViewFilter>), "param")]
        [ActionName("GetInStockMedicineWithTypeTreeOrderByExpiredDate")]
        public ApiResult GetInStockMedicineWithTypeTreeOrderByExpiredDate(ApiParam<HisMedicineStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<List<HisMedicineInStockSDO>>> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                    result = mng.GetInStockMedicineWithTypeTreeOrderByExpiredDate(param.ApiData);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisMedicineSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE> result = null;
                if (param != null)
                {
                    HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                    result = mng.UpdateSdo(param.ApiData);
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
        public ApiResult Lock(ApiParam<HisMedicineChangeLockSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Unlock")]
        public ApiResult Unlock(ApiParam<HisMedicineChangeLockSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                result = mng.Unlock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("ReturnAvailable")]
        public ApiResult ReturnAvailable(ApiParam<HisMedicineReturnAvailableSDO> param)
        {
            ApiResultObject<bool> result = null;
            if (param != null)
            {
                HisMedicineManager mng = new HisMedicineManager(param.CommonParam);
                result = mng.ReturnAvailable(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
