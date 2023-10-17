using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisConfig;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisConfigController : BaseApiController
    {
        [HttpPost]
        [ActionName("ResetAll")]
        public ApiResult ResetAll()
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);

                HisConfigManager mng = new HisConfigManager();
                result = mng.ResetAll();
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<HisConfigFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisConfigFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CONFIG>> result = new ApiResultObject<List<HIS_CONFIG>>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CONFIG> param)
        {
            try
            {
                ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_CONFIG> param)
        {
            try
            {
                ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_CONFIG> result = new ApiResultObject<HIS_CONFIG>(null);
                if (param != null && param.ApiData != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_CONFIG>> param)
        {
            try
            {
                ApiResultObject<List<HIS_CONFIG>> result = new ApiResultObject<List<HIS_CONFIG>>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_CONFIG>> param)
        {
            try
            {
                ApiResultObject<List<HIS_CONFIG>> result = new ApiResultObject<List<HIS_CONFIG>>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
        [AllowAnonymous]
        [ApiParamFilter(typeof(ApiParam<HisConfigViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisConfigViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CONFIG>> result = new ApiResultObject<List<V_HIS_CONFIG>>(null);
                if (param != null)
                {
                    HisConfigManager mng = new HisConfigManager(param.CommonParam);
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

    }
}
