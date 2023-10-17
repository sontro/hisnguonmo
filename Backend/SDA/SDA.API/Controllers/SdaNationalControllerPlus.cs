using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaNationalController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalViewFilterQuery>), "param")]
        [ActionName("GetView")]
        [AllowAnonymous]
        public ApiResult GetView(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_NATIONAL>> result = new ApiResultObject<List<V_SDA_NATIONAL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SDA_NATIONAL> resultData = managerContainer.Run<List<V_SDA_NATIONAL>>();
                    result = PackResult<List<V_SDA_NATIONAL>>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        [AllowAnonymous]
        public ApiResultZip GetViewZip(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_NATIONAL>> result = new ApiResultObject<List<V_SDA_NATIONAL>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SDA_NATIONAL> resultData = managerContainer.Run<List<V_SDA_NATIONAL>>();
                    result = PackResult<List<V_SDA_NATIONAL>>(resultData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalFilterQuery>), "param")]
        [ActionName("GetDynamic")]
        [AllowAnonymous]
        public ApiResultZip GetDynamic(ApiParam<SDA.MANAGER.Core.SdaNational.Get.SdaNationalFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<object>> result = new ApiResultObject<List<object>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaNationalManager), "GetDynamic", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<object> resultData = managerContainer.Run<List<object>>();
                    result = PackResult<List<object>>(resultData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
