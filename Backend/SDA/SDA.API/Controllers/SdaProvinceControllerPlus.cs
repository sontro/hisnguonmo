using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaProvinceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        [AllowAnonymous]
        public ApiResult GetView(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_PROVINCE>> result = new ApiResultObject<List<V_SDA_PROVINCE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SDA_PROVINCE> resultData = managerContainer.Run<List<V_SDA_PROVINCE>>();
                    result = PackResult<List<V_SDA_PROVINCE>>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        [AllowAnonymous]
        public ApiResultZip GetViewZip(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_PROVINCE>> result = new ApiResultObject<List<V_SDA_PROVINCE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SDA_PROVINCE> resultData = managerContainer.Run<List<V_SDA_PROVINCE>>();
                    result = PackResult<List<V_SDA_PROVINCE>>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery>), "param")]
        [ActionName("GetViewDynamic")]
        [AllowAnonymous]
        public ApiResultZip GetViewDynamic(ApiParam<SDA.MANAGER.Core.SdaProvince.Get.SdaProvinceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<object>> result = new ApiResultObject<List<object>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaProvinceManager), "GetDynamic", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
