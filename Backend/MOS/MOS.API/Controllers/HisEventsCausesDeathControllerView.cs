using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEventsCausesDeath;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEventsCausesDeathController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEventsCausesDeathViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEventsCausesDeathViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EVENTS_CAUSES_DEATH>> result = new ApiResultObject<List<V_HIS_EVENTS_CAUSES_DEATH>>(null);
                if (param != null)
                {
                    HisEventsCausesDeathManager mng = new HisEventsCausesDeathManager(param.CommonParam);
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
