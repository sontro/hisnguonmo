using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPtttCalendar;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPtttCalendarController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPtttCalendarViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisPtttCalendarViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PTTT_CALENDAR>> result = new ApiResultObject<List<V_HIS_PTTT_CALENDAR>>(null);
                if (param != null)
                {
                    HisPtttCalendarManager mng = new HisPtttCalendarManager(param.CommonParam);
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
