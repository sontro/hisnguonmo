using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaNotifyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaNotify.Get.SdaNotifyFilterQuery>), "param")]
        [ActionName("Get")]
        //[AllowAnonymous]
        public ApiResult Get(ApiParam<SDA.MANAGER.Core.SdaNotify.Get.SdaNotifyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SDA_NOTIFY>> result = new ApiResultObject<List<SDA_NOTIFY>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_NOTIFY> resultData = managerContainer.Run<List<SDA_NOTIFY>>();
                    result = PackResult<List<SDA_NOTIFY>>(resultData);
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
        public ApiResult Create(ApiParam<SDA_NOTIFY> param)
        {
            try
            {
                ApiResultObject<SDA_NOTIFY> result = new ApiResultObject<SDA_NOTIFY>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NOTIFY resultData = managerContainer.Run<SDA_NOTIFY>();
                    result = PackResult<SDA_NOTIFY>(resultData);
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
        public ApiResult Update(ApiParam<SDA_NOTIFY> param)
        {
            try
            {
                ApiResultObject<SDA_NOTIFY> result = new ApiResultObject<SDA_NOTIFY>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NOTIFY resultData = managerContainer.Run<SDA_NOTIFY>();
                    result = PackResult<SDA_NOTIFY>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SDA_NOTIFY> param)
        {
            try
            {
                ApiResultObject<SDA_NOTIFY> result = new ApiResultObject<SDA_NOTIFY>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_NOTIFY resultData = managerContainer.Run<SDA_NOTIFY>();
                    result = PackResult<SDA_NOTIFY>(resultData);
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
                    SDA_NOTIFY data = new SDA_NOTIFY();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
        [ActionName("NotifySeen")]
        public ApiResult NotifySeen(ApiParam<SDA.SDO.SdaNotifySeenSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "NotifySeen", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        public ApiResult CreateList(ApiParam<List<SDA_NOTIFY>> param)
        {
            try
            {
                ApiResultObject<List<SDA_NOTIFY>> result = new ApiResultObject<List<SDA_NOTIFY>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaNotifyManager), "CreateList", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_NOTIFY> resultData = managerContainer.Run<List<SDA_NOTIFY>>();
                    result = PackResult<List<SDA_NOTIFY>>(resultData);
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
