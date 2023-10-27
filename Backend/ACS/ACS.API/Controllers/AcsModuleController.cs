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
    public partial class AcsModuleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_MODULE>> result = new ApiResultObject<List<ACS_MODULE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_MODULE> resultData = managerContainer.Run<List<ACS_MODULE>>();
                    result = PackResult<List<ACS_MODULE>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        //[HttpGet]
        //[ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleViewFilterQuery>), "param")]
        //[ActionName("GetView")]
        //public ApiResult GetView(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleViewFilterQuery> param)
        //{
        //    try
        //    {
        //        ApiResultObject<List<object>> result = new ApiResultObject<List<object>>(null, false);
        //        if (param != null)
        //        {
        //            if (param.CommonParam == null) param.CommonParam = new CommonParam();
        //            this.commonParam = param.CommonParam;
        //            AcsModuleManager managerContainer = new AcsModuleManager(param.CommonParam);
        //            List<object> resultData = managerContainer.GetV(param.ApiData);
        //            result = PackResult<List<object>>(resultData);
        //        }
        //        return new ApiResult(result, this.ActionContext);
        //    }
        //    catch (Exception ex)
        //    {
        //        Inventec.Common.Logging.LogSystem.Error(ex);
        //        return null;
        //    }
        //}

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<ACS.MANAGER.Core.AcsModule.Get.AcsModuleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_MODULE>> result = new ApiResultObject<List<V_ACS_MODULE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_ACS_MODULE> resultData = managerContainer.Run<List<V_ACS_MODULE>>();
                    result = PackResult<List<V_ACS_MODULE>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_MODULE> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE> result = new ApiResultObject<ACS_MODULE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE resultData = managerContainer.Run<ACS_MODULE>();
                    result = PackResult<ACS_MODULE>(resultData);
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
        public ApiResult Update(ApiParam<ACS_MODULE> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE> result = new ApiResultObject<ACS_MODULE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE resultData = managerContainer.Run<ACS_MODULE>();
                    result = PackResult<ACS_MODULE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_MODULE> param)
        {
            try
            {
                ApiResultObject<ACS_MODULE> result = new ApiResultObject<ACS_MODULE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_MODULE resultData = managerContainer.Run<ACS_MODULE>();
                    result = PackResult<ACS_MODULE>(resultData);
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
        public ApiResult Delete(ApiParam<ACS_MODULE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsModuleManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
