using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicalAssessment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMedicalAssessmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicalAssessmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMedicalAssessmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICAL_ASSESSMENT>> result = new ApiResultObject<List<V_HIS_MEDICAL_ASSESSMENT>>(null);
                if (param != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
