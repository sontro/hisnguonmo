using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisImpMestBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestBloodFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestBloodFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_BLOOD>> result = new ApiResultObject<List<HIS_IMP_MEST_BLOOD>>(null);
                if (param != null)
                {
                    HisImpMestBloodManager mng = new HisImpMestBloodManager(param.CommonParam);
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
