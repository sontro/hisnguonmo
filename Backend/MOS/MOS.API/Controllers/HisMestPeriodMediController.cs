using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodMedi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestPeriodMediController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMediFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPeriodMediFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PERIOD_MEDI>> result = new ApiResultObject<List<HIS_MEST_PERIOD_MEDI>>(null);
                if (param != null)
                {
                    HisMestPeriodMediManager mng = new HisMestPeriodMediManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodMediViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestPeriodMediViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PERIOD_MEDI>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_MEDI>>(null);
                if (param != null)
                {
                    HisMestPeriodMediManager mng = new HisMestPeriodMediManager(param.CommonParam);
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
