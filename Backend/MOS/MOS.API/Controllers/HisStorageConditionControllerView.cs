using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisStorageCondition;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisStorageConditionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisStorageConditionViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisStorageConditionViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_STORAGE_CONDITION>> result = new ApiResultObject<List<V_HIS_STORAGE_CONDITION>>(null);
                if (param != null)
                {
                    HisStorageConditionManager mng = new HisStorageConditionManager(param.CommonParam);
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
