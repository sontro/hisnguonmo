using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestMatyReq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestMatyReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestMatyReqViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExpMestMatyReqViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MATY_REQ>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MATY_REQ>>(null);
                if (param != null)
                {
                    HisExpMestMatyReqManager mng = new HisExpMestMatyReqManager(param.CommonParam);
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
