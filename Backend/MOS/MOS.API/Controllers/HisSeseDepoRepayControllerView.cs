using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSeseDepoRepay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSeseDepoRepayController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSeseDepoRepayViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSeseDepoRepayViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SESE_DEPO_REPAY>> result = new ApiResultObject<List<V_HIS_SESE_DEPO_REPAY>>(null);
                if (param != null)
                {
                    HisSeseDepoRepayManager mng = new HisSeseDepoRepayManager(param.CommonParam);
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
