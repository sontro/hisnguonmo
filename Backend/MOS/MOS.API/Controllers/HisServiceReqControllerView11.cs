using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceReqView11FilterQuery>), "param")]
        [ActionName("GetView11")]
        public ApiResult GetView11(ApiParam<HisServiceReqView11FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_REQ_11>> result = new ApiResultObject<List<V_HIS_SERVICE_REQ_11>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetView11(param.ApiData);
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
