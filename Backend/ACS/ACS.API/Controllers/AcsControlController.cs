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
    public partial class AcsControlController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsControl.Get.AcsControlFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsControl.Get.AcsControlFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_CONTROL>> result = new ApiResultObject<List<ACS_CONTROL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsControlManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_CONTROL> resultData = managerContainer.Run<List<ACS_CONTROL>>();
                    result = PackResult<List<ACS_CONTROL>>(resultData);
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
        public ApiResult Create(ApiParam<ACS_CONTROL> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL> result = new ApiResultObject<ACS_CONTROL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsControlManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL resultData = managerContainer.Run<ACS_CONTROL>();
                    result = PackResult<ACS_CONTROL>(resultData);
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
        public ApiResult Update(ApiParam<ACS_CONTROL> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL> result = new ApiResultObject<ACS_CONTROL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsControlManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL resultData = managerContainer.Run<ACS_CONTROL>();
                    result = PackResult<ACS_CONTROL>(resultData);
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
        public ApiResult ChangeLock(ApiParam<ACS_CONTROL> param)
        {
            try
            {
                ApiResultObject<ACS_CONTROL> result = new ApiResultObject<ACS_CONTROL>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsControlManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_CONTROL resultData = managerContainer.Run<ACS_CONTROL>();
                    result = PackResult<ACS_CONTROL>(resultData);
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
        public ApiResult Delete(ApiParam<ACS_CONTROL> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsControlManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
