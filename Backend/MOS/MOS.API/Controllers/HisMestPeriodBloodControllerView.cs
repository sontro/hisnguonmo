using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPeriodBlood;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMestPeriodBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPeriodBloodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMestPeriodBloodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PERIOD_BLOOD>> result = new ApiResultObject<List<V_HIS_MEST_PERIOD_BLOOD>>(null);
                if (param != null)
                {
                    HisMestPeriodBloodManager mng = new HisMestPeriodBloodManager(param.CommonParam);
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
