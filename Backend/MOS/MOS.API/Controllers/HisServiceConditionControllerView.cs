using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceCondition;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceConditionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceConditionViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceConditionViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_CONDITION>> result = new ApiResultObject<List<V_HIS_SERVICE_CONDITION>>(null);
                if (param != null)
                {
                    HisServiceConditionManager mng = new HisServiceConditionManager(param.CommonParam);
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
