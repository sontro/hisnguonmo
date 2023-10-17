using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisImpMestSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_STT>> result = new ApiResultObject<List<HIS_IMP_MEST_STT>>(null);
                if (param != null)
                {
                    HisImpMestSttManager mng = new HisImpMestSttManager(param.CommonParam);
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
