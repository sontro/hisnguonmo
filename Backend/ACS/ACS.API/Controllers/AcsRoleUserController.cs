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
    public partial class AcsRoleUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_ROLE_USER>> result = new ApiResultObject<List<ACS_ROLE_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_ROLE_USER> resultData = managerContainer.Run<List<ACS_ROLE_USER>>();
                    result = PackResult<List<ACS_ROLE_USER>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_ROLE_USER> param)
        {
            try
            {
                ApiResultObject<ACS_ROLE_USER> result = new ApiResultObject<ACS_ROLE_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_ROLE_USER resultData = managerContainer.Run<ACS_ROLE_USER>();
                    result = PackResult<ACS_ROLE_USER>(resultData);
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
        public ApiResult Update(ApiParam<ACS_ROLE_USER> param)
        {
            try
            {
                ApiResultObject<ACS_ROLE_USER> result = new ApiResultObject<ACS_ROLE_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_ROLE_USER resultData = managerContainer.Run<ACS_ROLE_USER>();
                    result = PackResult<ACS_ROLE_USER>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_ROLE_USER> param)
        {
            try
            {
                ApiResultObject<ACS_ROLE_USER> result = new ApiResultObject<ACS_ROLE_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_ROLE_USER resultData = managerContainer.Run<ACS_ROLE_USER>();
                    result = PackResult<ACS_ROLE_USER>(resultData);
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
        public ApiResult Delete(ApiParam<ACS_ROLE_USER> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
