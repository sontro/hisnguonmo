using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentLogging;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentLoggingController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentLoggingViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisTreatmentLoggingViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_LOGGING>> result = new ApiResultObject<List<V_HIS_TREATMENT_LOGGING>>(null);
                if (param != null)
                {
                    HisTreatmentLoggingManager mng = new HisTreatmentLoggingManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }
    }
}
