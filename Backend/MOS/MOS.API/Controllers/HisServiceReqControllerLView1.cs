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
        [ApiParamFilter(typeof(ApiParam<HisServiceReqLView1FilterQuery>), "param")]
        [ActionName("GetLView1")]
        public ApiResult GetLView1(ApiParam<HisServiceReqLView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<L_HIS_SERVICE_REQ_1>> result = new ApiResultObject<List<L_HIS_SERVICE_REQ_1>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetLView1(param.ApiData);
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
