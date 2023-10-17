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
        [ApiParamFilter(typeof(ApiParam<HisExpMestBltyReqView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisExpMestBltyReqView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_BLTY_REQ_1>> result = new ApiResultObject<List<V_HIS_EXP_MEST_BLTY_REQ_1>>(null);
                if (param != null)
                {
                    HisExpMestBltyReqManager mng = new HisExpMestBltyReqManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
