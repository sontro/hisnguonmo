using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceChangeReq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceChangeReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceChangeReqViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceChangeReqViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_CHANGE_REQ>> result = new ApiResultObject<List<V_HIS_SERVICE_CHANGE_REQ>>(null);
                if (param != null)
                {
                    HisServiceChangeReqManager mng = new HisServiceChangeReqManager(param.CommonParam);
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
