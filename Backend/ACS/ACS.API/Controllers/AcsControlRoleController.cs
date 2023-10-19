using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsControlRoleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsControlRole.Get.AcsControlRoleFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsControlRole.Get.AcsControlRoleFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_CONTROL_ROLE>> result = new ApiResultObject<List<ACS_CONTROL_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_CONTROL_ROLE> resultData = managerContainer.Run<List<ACS_CONTROL_ROLE>>();
                    result = PackResult<List<ACS_CONTROL_ROLE>>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsControlRole.Get.AcsControlRoleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsControlRole.Get.AcsControlRoleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_CONTROL_ROLE>> result = new ApiResultObject<List<V_ACS_CONTROL_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_ACS_CONTROL_ROLE> resultData = managerContainer.Run<List<V_ACS_CONTROL_ROLE>>();
                    result = PackResult<List<V_ACS_CONTROL_ROLE>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_CONTROL_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL_ROLE> result = new ApiResultObject<ACS_CONTROL_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL_ROLE resultData = managerContainer.Run<ACS_CONTROL_ROLE>();
                    result = PackResult<ACS_CONTROL_ROLE>(resultData);
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
        public ApiResult CreateList(ApiParam<List<ACS_CONTROL_ROLE>> param)
        {
            try
            {
                ApiResultObject<List<ACS_CONTROL_ROLE>> result = new ApiResultObject<List<ACS_CONTROL_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_CONTROL_ROLE> resultData = managerContainer.Run<List<ACS_CONTROL_ROLE>>();
                    result = PackResult<List<ACS_CONTROL_ROLE>>(resultData);
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
        public ApiResult Update(ApiParam<ACS_CONTROL_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL_ROLE> result = new ApiResultObject<ACS_CONTROL_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL_ROLE resultData = managerContainer.Run<ACS_CONTROL_ROLE>();
                    result = PackResult<ACS_CONTROL_ROLE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_CONTROL_ROLE> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL_ROLE> result = new ApiResultObject<ACS_CONTROL_ROLE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL_ROLE resultData = managerContainer.Run<ACS_CONTROL_ROLE>();
                    result = PackResult<ACS_CONTROL_ROLE>(resultData);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ACS_CONTROL_ROLE data = new ACS_CONTROL_ROLE();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsControlRoleManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
