using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;
using SDA.SDO;

namespace SDA.API.Controllers
{
    public partial class SdaLicenseController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SDA.MANAGER.Core.SdaLicense.Get.SdaLicenseFilterQuery>), "param")]
        [ActionName("GetLast")]
        [AllowAnonymous]
        public ApiResult GetLast(ApiParam<SDA.MANAGER.Core.SdaLicense.Get.SdaLicenseFilterQuery> param)
        {
            try
            {
                ApiResultObject<SDA_LICENSE> result = new ApiResultObject<SDA_LICENSE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "GetLast", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SDA_LICENSE resultData = managerContainer.Run<SDA_LICENSE>();
                    result = PackResult<SDA_LICENSE>(resultData);
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
        [ActionName("Decode")]
        public ApiResult Decode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<SdaLicenseSDO> result = new ApiResultObject<SdaLicenseSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaLicenseManager), "Decode", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SdaLicenseSDO resultData = managerContainer.Run<SdaLicenseSDO>();
                    result = PackResult<SdaLicenseSDO>(resultData);
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
