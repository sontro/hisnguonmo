using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceNumOrder;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceNumOrderController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceNumOrderViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceNumOrderViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_NUM_ORDER>> result = new ApiResultObject<List<V_HIS_SERVICE_NUM_ORDER>>(null);
                if (param != null)
                {
                    HisServiceNumOrderManager mng = new HisServiceNumOrderManager(param.CommonParam);
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
