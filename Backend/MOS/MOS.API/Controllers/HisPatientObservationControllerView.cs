using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientObservation;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPatientObservationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientObservationViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPatientObservationViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_OBSERVATION>> result = new ApiResultObject<List<V_HIS_PATIENT_OBSERVATION>>(null);
                if (param != null)
                {
                    HisPatientObservationManager mng = new HisPatientObservationManager(param.CommonParam);
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
