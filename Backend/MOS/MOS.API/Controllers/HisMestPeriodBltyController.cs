using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodBlty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMestPeriodBltyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodBltyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPeriodBltyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PERIOD_BLTY>> result = new ApiResultObject<List<HIS_MEST_PERIOD_BLTY>>(null);
                if (param != null)
                {
                    HisMestPeriodBltyManager mng = new HisMestPeriodBltyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodBltyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMestPeriodBltyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PERIOD_BLTY>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_BLTY>>(null);
                if (param != null)
                {
                    HisMestPeriodBltyManager mng = new HisMestPeriodBltyManager(param.CommonParam);
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
