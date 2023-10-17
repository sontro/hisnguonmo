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
        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SDA_CONFIG_APP_USER>> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG_APP_USER> resultData = managerContainer.Run<List<SDA_CONFIG_APP_USER>>();
                    result = PackResult<List<SDA_CONFIG_APP_USER>>(resultData);
                }
                //Inventec.Common.Logging.LogSystem.Info("CreateList => " + Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => result), result));
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SDA_CONFIG_APP_USER>> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG_APP_USER>> result = new ApiResultObject<List<SDA_CONFIG_APP_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaConfigAppUserManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
