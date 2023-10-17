using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaConfigAppUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaConfigAppUser.Get.SdaConfigAppUserFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaConfigAppUser.Get.SdaConfigAppUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG_APP_USER> resultData = managerContainer.Run<List<SDA_CONFIG_APP_USER>>();
                    result = PackResult<List<SDA_CONFIG_APP_USER>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_CONFIG_APP_USER> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG_APP_USER> result = new ApiResultObject<SDA_CONFIG_APP_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG_APP_USER resultData = managerContainer.Run<SDA_CONFIG_APP_USER>();
                    result = PackResult<SDA_CONFIG_APP_USER>(resultData);
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
        public ApiResult Update(ApiParam<SDA_CONFIG_APP_USER> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG_APP_USER> result = new ApiResultObject<SDA_CONFIG_APP_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG_APP_USER resultData = managerContainer.Run<SDA_CONFIG_APP_USER>();
                    result = PackResult<SDA_CONFIG_APP_USER>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_CONFIG_APP_USER> param)
        {
            try
            {
                ApiResultObject<SDA_CONFIG_APP_USER> result = new ApiResultObject<SDA_CONFIG_APP_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_CONFIG_APP_USER resultData = managerContainer.Run<SDA_CONFIG_APP_USER>();
                    result = PackResult<SDA_CONFIG_APP_USER>(resultData);
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
                    SDA_CONFIG_APP_USER data = new SDA_CONFIG_APP_USER();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
        [ActionName("CopyByConfig")]
        public ApiResult CopyByConfig(ApiParam<SDO.SdaConfigAppUserCopyByConfigSDO> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigAppUserManager), "Copy", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG_APP_USER> resultData = managerContainer.Run<List<SDA_CONFIG_APP_USER>>();
                    result = PackResult<List<SDA_CONFIG_APP_USER>>(resultData);
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
        [ActionName("CopyByUser")]
        public ApiResult CopyByUser(ApiParam<SDO.SdaConfigAppUserCopyByUserSDO> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigAppUserManager), "Copy", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG_APP_USER> resultData = managerContainer.Run<List<SDA_CONFIG_APP_USER>>();
                    result = PackResult<List<SDA_CONFIG_APP_USER>>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaConfigAppUser.Get.SdaConfigAppUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<SDA.MANAGER.Core.SdaConfigAppUser.Get.SdaConfigAppUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<V_SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "View", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SDA_CONFIG_APP_USER> resultData = managerContainer.Run<List<V_SDA_CONFIG_APP_USER>>();
                    result = PackResult<List<V_SDA_CONFIG_APP_USER>>(resultData);
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
