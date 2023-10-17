using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMediStock;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMediStockController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediStockFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMediStockFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDI_STOCK>> result = new ApiResultObject<List<HIS_MEDI_STOCK>>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMediStockViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK>>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMediStockReplaceSDOFilter>), "param")]
        [ActionName("GetReplaceSDO")]
        public ApiResult GetReplaceSDO(ApiParam<HisMediStockReplaceSDOFilter> param)
        {
            try
            {
                ApiResultObject<HisMediStockReplaceSDO> result = new ApiResultObject<HisMediStockReplaceSDO>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
                    result = mng.GetReplaceSDO(param.ApiData);
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
        public ApiResult Create(ApiParam<HisMediStockSDO> param)
        {
            try
            {
                ApiResultObject<HisMediStockSDO> result = new ApiResultObject<HisMediStockSDO>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisMediStockSDO> param)
        {
            try
            {
                ApiResultObject<HisMediStockSDO> result = new ApiResultObject<HisMediStockSDO>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEDI_STOCK> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_MEDI_STOCK> param)
        {
            try
            {
                ApiResultObject<HIS_MEDI_STOCK> result = new ApiResultObject<HIS_MEDI_STOCK>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HisMediStockSDO>> param)
        {
            try
            {
                ApiResultObject<List<HisMediStockSDO>> result = new ApiResultObject<List<HisMediStockSDO>>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        [ActionName("Inventory")]
        public ApiResult Inventory(ApiParam<HisMediStockInventorySDO> param)
        {
            try
            {
                ApiResultObject<HisMediStockInventoryResultSDO> result = new ApiResultObject<HisMediStockInventoryResultSDO>(null);
                if (param != null)
                {
                    HisMediStockManager mng = new HisMediStockManager(param.CommonParam);
                    result = mng.Inventory(param.ApiData);
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
