using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServicePackage;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServicePackageController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServicePackageFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServicePackageFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_PACKAGE>> result = new ApiResultObject<List<HIS_SERVICE_PACKAGE>>(null);
                if (param != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServicePackageViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServicePackageViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_PACKAGE>> result = new ApiResultObject<List<V_HIS_SERVICE_PACKAGE>>(null);
                if (param != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_SERVICE_PACKAGE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
                if (param != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_PACKAGE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
                if (param != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERVICE_PACKAGE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_SERVICE_PACKAGE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_PACKAGE> result = new ApiResultObject<HIS_SERVICE_PACKAGE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServicePackageManager mng = new HisServicePackageManager(param.CommonParam);
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
