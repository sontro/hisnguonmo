using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpiredDateCfg;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpiredDateCfgController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpiredDateCfgViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisExpiredDateCfgViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXPIRED_DATE_CFG>> result = new ApiResultObject<List<V_HIS_EXPIRED_DATE_CFG>>(null);
                if (param != null)
                {
                    HisExpiredDateCfgManager mng = new HisExpiredDateCfgManager(param.CommonParam);
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
