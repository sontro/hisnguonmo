using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExmeReasonCfg;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExmeReasonCfgController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExmeReasonCfgViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisExmeReasonCfgViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXME_REASON_CFG>> result = new ApiResultObject<List<V_HIS_EXME_REASON_CFG>>(null);
                if (param != null)
                {
                    HisExmeReasonCfgManager mng = new HisExmeReasonCfgManager(param.CommonParam);
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
