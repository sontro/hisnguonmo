using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisManufacturer;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisManufacturerController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisManufacturerFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisManufacturerFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MANUFACTURER>> result = new ApiResultObject<List<HIS_MANUFACTURER>>(null);
                if (param != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MANUFACTURER> param)
        {
            try
            {
                ApiResultObject<HIS_MANUFACTURER> result = new ApiResultObject<HIS_MANUFACTURER>(null);
                if (param != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MANUFACTURER>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MANUFACTURER>> result = new ApiResultObject<List<HIS_MANUFACTURER>>(null);
                if (param != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MANUFACTURER> param)
        {
            try
            {
                ApiResultObject<HIS_MANUFACTURER> result = new ApiResultObject<HIS_MANUFACTURER>(null);
                if (param != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MANUFACTURER> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_MANUFACTURER> param)
        {
            try
            {
                ApiResultObject<HIS_MANUFACTURER> result = new ApiResultObject<HIS_MANUFACTURER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisManufacturerManager mng = new HisManufacturerManager(param.CommonParam);
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
