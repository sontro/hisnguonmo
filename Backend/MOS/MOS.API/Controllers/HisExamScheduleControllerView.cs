using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExamSchedule;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExamScheduleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExamScheduleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisExamScheduleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXAM_SCHEDULE>> result = new ApiResultObject<List<V_HIS_EXAM_SCHEDULE>>(null);
                if (param != null)
                {
                    HisExamScheduleManager mng = new HisExamScheduleManager(param.CommonParam);
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
