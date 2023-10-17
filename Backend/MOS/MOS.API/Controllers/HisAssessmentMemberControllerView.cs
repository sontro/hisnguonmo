using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAssessmentMember;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAssessmentMemberController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAssessmentMemberViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAssessmentMemberViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ASSESSMENT_MEMBER>> result = new ApiResultObject<List<V_HIS_ASSESSMENT_MEMBER>>(null);
                if (param != null)
                {
                    HisAssessmentMemberManager mng = new HisAssessmentMemberManager(param.CommonParam);
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
