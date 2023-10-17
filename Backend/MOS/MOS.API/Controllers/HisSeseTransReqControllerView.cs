using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSeseTransReq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSeseTransReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSeseTransReqViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSeseTransReqViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SESE_TRANS_REQ>> result = new ApiResultObject<List<V_HIS_SESE_TRANS_REQ>>(null);
                if (param != null)
                {
                    HisSeseTransReqManager mng = new HisSeseTransReqManager(param.CommonParam);
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
