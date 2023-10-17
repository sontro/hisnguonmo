using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceSame;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceSameController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceSameViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceSameViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_SAME>> result = new ApiResultObject<List<V_HIS_SERVICE_SAME>>(null);
                if (param != null)
                {
                    HisServiceSameManager mng = new HisServiceSameManager(param.CommonParam);
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
