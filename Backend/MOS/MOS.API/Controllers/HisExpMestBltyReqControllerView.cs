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
    public partial class HisExpMestBltyReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestBltyReqViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExpMestBltyReqViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_BLTY_REQ>> result = new ApiResultObject<List<V_HIS_EXP_MEST_BLTY_REQ>>(null);
                if (param != null)
                {
                    HisExpMestBltyReqManager mng = new HisExpMestBltyReqManager(param.CommonParam);
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
