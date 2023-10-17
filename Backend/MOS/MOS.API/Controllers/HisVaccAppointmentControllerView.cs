using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccAppointment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisVaccAppointmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccAppointmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisVaccAppointmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_VACC_APPOINTMENT>> result = new ApiResultObject<List<V_HIS_VACC_APPOINTMENT>>(null);
                if (param != null)
                {
                    HisVaccAppointmentManager mng = new HisVaccAppointmentManager(param.CommonParam);
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
