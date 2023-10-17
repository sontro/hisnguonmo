using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceRereTime;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceRereTimeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceRereTimeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceRereTimeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_RERE_TIME>> result = new ApiResultObject<List<V_HIS_SERVICE_RERE_TIME>>(null);
                if (param != null)
                {
                    HisServiceRereTimeManager mng = new HisServiceRereTimeManager(param.CommonParam);
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
