using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReqStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceReqSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceReqSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceReqSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_REQ_STT>> result = new ApiResultObject<List<HIS_SERVICE_REQ_STT>>(null);
                if (param != null)
                {
                    HisServiceReqSttManager mng = new HisServiceReqSttManager(param.CommonParam);
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
