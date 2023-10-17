using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisExpMestSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST_STT>> result = new ApiResultObject<List<HIS_EXP_MEST_STT>>(null);
                if (param != null)
                {
                    HisExpMestSttManager mng = new HisExpMestSttManager(param.CommonParam);
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
