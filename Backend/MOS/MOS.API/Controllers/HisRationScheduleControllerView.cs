using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRationSchedule;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisRationScheduleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRationScheduleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisRationScheduleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_RATION_SCHEDULE>> result = new ApiResultObject<List<V_HIS_RATION_SCHEDULE>>(null);
                if (param != null)
                {
                    HisRationScheduleManager mng = new HisRationScheduleManager(param.CommonParam);
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
