using Inventec.Core;
using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaHideControlController : BaseApiController
    {
        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SDA_HIDE_CONTROL>> param)
        {
            try
            {
                ApiResultObject<List<SDA_HIDE_CONTROL>> result = new ApiResultObject<List<SDA_HIDE_CONTROL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaHideControlManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_HIDE_CONTROL> resultData = managerContainer.Run<List<SDA_HIDE_CONTROL>>();
                    result = PackResult<List<SDA_HIDE_CONTROL>>(resultData);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SDA_HIDE_CONTROL>> param)
        {
            try
            {
                ApiResultObject<List<SDA_HIDE_CONTROL>> result = new ApiResultObject<List<SDA_HIDE_CONTROL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaHideControlManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_HIDE_CONTROL> resultData = managerContainer.Run<List<SDA_HIDE_CONTROL>>();
                    result = PackResult<List<SDA_HIDE_CONTROL>>(resultData);
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
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(SdaHideControlManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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