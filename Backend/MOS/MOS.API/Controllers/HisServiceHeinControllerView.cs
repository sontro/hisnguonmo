using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceHein;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceHeinController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceHeinViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceHeinViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_HEIN>> result = new ApiResultObject<List<V_HIS_SERVICE_HEIN>>(null);
                if (param != null)
                {
                    HisServiceHeinManager mng = new HisServiceHeinManager(param.CommonParam);
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
