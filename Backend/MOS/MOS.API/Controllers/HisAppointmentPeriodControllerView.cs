using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAppointmentPeriod;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAppointmentPeriodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAppointmentPeriodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAppointmentPeriodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_APPOINTMENT_PERIOD>> result = new ApiResultObject<List<V_HIS_APPOINTMENT_PERIOD>>(null);
                if (param != null)
                {
                    HisAppointmentPeriodManager mng = new HisAppointmentPeriodManager(param.CommonParam);
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
