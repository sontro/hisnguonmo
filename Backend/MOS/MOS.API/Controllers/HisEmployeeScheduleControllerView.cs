using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmployeeSchedule;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEmployeeScheduleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEmployeeScheduleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEmployeeScheduleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EMPLOYEE_SCHEDULE>> result = new ApiResultObject<List<V_HIS_EMPLOYEE_SCHEDULE>>(null);
                if (param != null)
                {
                    HisEmployeeScheduleManager mng = new HisEmployeeScheduleManager(param.CommonParam);
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
