using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaConfigController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetByCode")]
        [AllowAnonymous]
        public ApiResult GetByCode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<List<SDA_CONFIG>> result = new ApiResultObject<List<SDA_CONFIG>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SdaConfigManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SDA_CONFIG> resultData = managerContainer.Run<List<SDA_CONFIG>>();
                    result = PackResult<List<SDA_CONFIG>>(resultData);
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
