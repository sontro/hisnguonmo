using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccinationExam;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisVaccinationExamController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccinationExamViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisVaccinationExamViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_VACCINATION_EXAM>> result = new ApiResultObject<List<V_HIS_VACCINATION_EXAM>>(null);
                if (param != null)
                {
                    HisVaccinationExamManager mng = new HisVaccinationExamManager(param.CommonParam);
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
