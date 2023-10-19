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
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsApplicationRole.Get.AcsApplicationRoleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<ACS.MANAGER.Core.AcsApplicationRole.Get.AcsApplicationRoleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_APPLICATION_ROLE>> result = new ApiResultObject<List<V_ACS_APPLICATION_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_ACS_APPLICATION_ROLE> resultData = managerContainer.Run<List<V_ACS_APPLICATION_ROLE>>();
                    result = PackResult<List<V_ACS_APPLICATION_ROLE>>(resultData);
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
        public ApiResult CreateList(ApiParam<List<ACS_APPLICATION_ROLE>> param)
        {
            try
            {
                ApiResultObject<List<ACS_APPLICATION_ROLE>> result = new ApiResultObject<List<ACS_APPLICATION_ROLE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsApplicationRoleManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
