using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisNumOrderBlock;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisNumOrderBlockController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisNumOrderBlockViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisNumOrderBlockViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_NUM_ORDER_BLOCK>> result = new ApiResultObject<List<V_HIS_NUM_ORDER_BLOCK>>(null);
                if (param != null)
                {
                    HisNumOrderBlockManager mng = new HisNumOrderBlockManager(param.CommonParam);
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
