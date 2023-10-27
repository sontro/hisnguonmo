using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsModuleGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsModuleGroup.Get.AcsModuleGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsModuleGroup.Get.AcsModuleGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_MODULE_GROUP>> result = new ApiResultObject<List<ACS_MODULE_GROUP>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsModuleGroupManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_MODULE_GROUP> resultData = managerContainer.Run<List<ACS_MODULE_GROUP>>();
                    result = PackResult<List<ACS_MODULE_GROUP>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_MODULE_GROUP> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE_GROUP> result = new ApiResultObject<ACS_MODULE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsModuleGroupManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE_GROUP resultData = managerContainer.Run<ACS_MODULE_GROUP>();
                    result = PackResult<ACS_MODULE_GROUP>(resultData);
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
        public ApiResult Update(ApiParam<ACS_MODULE_GROUP> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE_GROUP> result = new ApiResultObject<ACS_MODULE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsModuleGroupManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE_GROUP resultData = managerContainer.Run<ACS_MODULE_GROUP>();
                    result = PackResult<ACS_MODULE_GROUP>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_MODULE_GROUP> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE_GROUP> result = new ApiResultObject<ACS_MODULE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsModuleGroupManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE_GROUP resultData = managerContainer.Run<ACS_MODULE_GROUP>();
                    result = PackResult<ACS_MODULE_GROUP>(resultData);
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
                    ACS_MODULE_GROUP data = new ACS_MODULE_GROUP();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsModuleGroupManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
