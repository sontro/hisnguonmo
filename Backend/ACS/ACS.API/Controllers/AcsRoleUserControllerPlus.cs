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
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_ROLE_USER>> result = new ApiResultObject<List<V_ACS_ROLE_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_ACS_ROLE_USER> resultData = managerContainer.Run<List<V_ACS_ROLE_USER>>();
                    result = PackResult<List<V_ACS_ROLE_USER>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery>), "param")]
        [ActionName("GetForTree")]
        public ApiResult GetForTree(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS.SDO.AcsRoleUserSDO>> result = new ApiResultObject<List<ACS.SDO.AcsRoleUserSDO>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "GetForTree", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS.SDO.AcsRoleUserSDO> resultData = managerContainer.Run<List<ACS.SDO.AcsRoleUserSDO>>();
                    result = PackResult<List<ACS.SDO.AcsRoleUserSDO>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery>), "param")]
        [ActionName("GetDynamic")]
        public ApiResult GetDynamic(ApiParam<ACS.MANAGER.Core.AcsRoleUser.Get.AcsRoleUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<object>> result = new ApiResultObject<List<object>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    AcsRoleUserManager manager = new MANAGER.Manager.AcsRoleUserManager(param.CommonParam);
                    List<object> resultData = manager.GetDynamic(param.ApiData);
                    result = PackResult<List<object>>(resultData);
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
        [ActionName("UpdateWithRole")]
        public ApiResult UpdateWithRole(ApiParam<ACS.SDO.AcsRoleUserForUpdateSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "UpdateWithRole", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<ACS_ROLE_USER>> param)
        {
            try
            {
                ApiResultObject<List<ACS_ROLE_USER>> result = new ApiResultObject<List<ACS_ROLE_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsRoleUserManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsRoleUserManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
