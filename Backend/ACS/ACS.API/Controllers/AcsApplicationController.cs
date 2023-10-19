using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using Inventec.Backend.MANAGER;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsApplicationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsApplication.Get.AcsApplicationFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsApplication.Get.AcsApplicationFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_APPLICATION>> result = new ApiResultObject<List<ACS_APPLICATION>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_APPLICATION> resultData = managerContainer.Run<List<ACS_APPLICATION>>();
                    result = PackResult<List<ACS_APPLICATION>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_APPLICATION> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION> result = new ApiResultObject<ACS_APPLICATION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION resultData = managerContainer.Run<ACS_APPLICATION>();
                    result = PackResult<ACS_APPLICATION>(resultData);
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
        public ApiResult Update(ApiParam<ACS_APPLICATION> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION> result = new ApiResultObject<ACS_APPLICATION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION resultData = managerContainer.Run<ACS_APPLICATION>();
                    result = PackResult<ACS_APPLICATION>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_APPLICATION> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION> result = new ApiResultObject<ACS_APPLICATION>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION resultData = managerContainer.Run<ACS_APPLICATION>();
                    result = PackResult<ACS_APPLICATION>(resultData);
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
        public ApiResult Delete(ApiParam<ACS_APPLICATION> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
