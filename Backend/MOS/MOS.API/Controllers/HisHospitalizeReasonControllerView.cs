using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHospitalizeReason;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisHospitalizeReasonController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHospitalizeReasonViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisHospitalizeReasonViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HOSPITALIZE_REASON>> result = new ApiResultObject<List<V_HIS_HOSPITALIZE_REASON>>(null);
                if (param != null)
                {
                    HisHospitalizeReasonManager mng = new HisHospitalizeReasonManager(param.CommonParam);
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
