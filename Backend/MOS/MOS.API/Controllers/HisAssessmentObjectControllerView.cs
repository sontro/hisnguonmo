using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAssessmentObject;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAssessmentObjectController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAssessmentObjectViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAssessmentObjectViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ASSESSMENT_OBJECT>> result = new ApiResultObject<List<V_HIS_ASSESSMENT_OBJECT>>(null);
                if (param != null)
                {
                    HisAssessmentObjectManager mng = new HisAssessmentObjectManager(param.CommonParam);
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
