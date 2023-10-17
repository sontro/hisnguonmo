using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarReportTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetByCode")]
        [AllowAnonymous]
        public ApiResult GetByCode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE> result = new ApiResultObject<SAR_REPORT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE resultData = managerContainer.Run<SAR_REPORT_TYPE>();
                    result = PackResult<SAR_REPORT_TYPE>(resultData);
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
