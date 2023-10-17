using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSupplier;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSupplierController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSupplierFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSupplierFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SUPPLIER>> result = new ApiResultObject<List<HIS_SUPPLIER>>(null);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SUPPLIER> param)
        {
            try
            {
                ApiResultObject<HIS_SUPPLIER> result = new ApiResultObject<HIS_SUPPLIER>(null);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_SUPPLIER>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SUPPLIER>> result = new ApiResultObject<List<HIS_SUPPLIER>>(null);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_SUPPLIER> param)
        {
            try
            {
                ApiResultObject<HIS_SUPPLIER> result = new ApiResultObject<HIS_SUPPLIER>(null);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SUPPLIER> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
        public ApiResult ChangeLock(ApiParam<HIS_SUPPLIER> param)
        {
            try
            {
                ApiResultObject<HIS_SUPPLIER> result = new ApiResultObject<HIS_SUPPLIER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSupplierManager mng = new HisSupplierManager(param.CommonParam);
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

    }
}
