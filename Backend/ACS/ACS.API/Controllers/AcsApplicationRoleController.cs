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
    public partial class AcsApplicationRoleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsApplicationRole.Get.AcsApplicationRoleFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsApplicationRole.Get.AcsApplicationRoleFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_APPLICATION_ROLE>> result = new ApiResultObject<List<ACS_APPLICATION_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_APPLICATION_ROLE> resultData = managerContainer.Run<List<ACS_APPLICATION_ROLE>>();
                    result = PackResult<List<ACS_APPLICATION_ROLE>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_APPLICATION_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION_ROLE> result = new ApiResultObject<ACS_APPLICATION_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION_ROLE resultData = managerContainer.Run<ACS_APPLICATION_ROLE>();
                    result = PackResult<ACS_APPLICATION_ROLE>(resultData);
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
        public ApiResult Update(ApiParam<ACS_APPLICATION_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION_ROLE> result = new ApiResultObject<ACS_APPLICATION_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION_ROLE resultData = managerContainer.Run<ACS_APPLICATION_ROLE>();
                    result = PackResult<ACS_APPLICATION_ROLE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_APPLICATION_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_APPLICATION_ROLE> result = new ApiResultObject<ACS_APPLICATION_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_APPLICATION_ROLE resultData = managerContainer.Run<ACS_APPLICATION_ROLE>();
                    result = PackResult<ACS_APPLICATION_ROLE>(resultData);
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
        public ApiResult Delete(ApiParam<ACS_APPLICATION_ROLE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
