using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestBltyReq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisExpMestBltyReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestBltyReqFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestBltyReqFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST_BLTY_REQ>> result = new ApiResultObject<List<HIS_EXP_MEST_BLTY_REQ>>(null);
                if (param != null)
                {
                    HisExpMestBltyReqManager mng = new HisExpMestBltyReqManager(param.CommonParam);
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
    }
}
