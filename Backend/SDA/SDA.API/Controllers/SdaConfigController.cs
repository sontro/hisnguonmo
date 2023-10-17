using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaConfigController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaConfig.Get.SdaConfigFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaConfig.Get.SdaConfigFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG>> result = new ApiResultObject<List<SDA_CONFIG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG> resultData = managerContainer.Run<List<SDA_CONFIG>>();
                    result = PackResult<List<SDA_CONFIG>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<SDA_CONFIG> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG> result = new ApiResultObject<SDA_CONFIG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG resultData = managerContainer.Run<SDA_CONFIG>();
                    result = PackResult<SDA_CONFIG>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<SDA_CONFIG> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG> result = new ApiResultObject<SDA_CONFIG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG resultData = managerContainer.Run<SDA_CONFIG>();
                    result = PackResult<SDA_CONFIG>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<SDA_CONFIG> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG> result = new ApiResultObject<SDA_CONFIG>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG resultData = managerContainer.Run<SDA_CONFIG>();
                    result = PackResult<SDA_CONFIG>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<SDA_CONFIG> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    bool resultData = managerContainer.Run<bool>();
                    result = PackResult<bool>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
