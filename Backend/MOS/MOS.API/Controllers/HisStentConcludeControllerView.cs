using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisStentConclude;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisStentConcludeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisStentConcludeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisStentConcludeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<V_HIS_STENT_CONCLUDE>>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
