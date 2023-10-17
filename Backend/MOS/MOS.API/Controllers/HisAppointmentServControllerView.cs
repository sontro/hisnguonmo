using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAppointmentServ;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAppointmentServController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAppointmentServViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAppointmentServViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_APPOINTMENT_SERV>> result = new ApiResultObject<List<V_HIS_APPOINTMENT_SERV>>(null);
                if (param != null)
                {
                    HisAppointmentServManager mng = new HisAppointmentServManager(param.CommonParam);
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
