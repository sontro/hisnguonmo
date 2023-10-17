using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBltyService;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBltyServiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBltyServiceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBltyServiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BLTY_SERVICE>> result = new ApiResultObject<List<V_HIS_BLTY_SERVICE>>(null);
                if (param != null)
                {
                    HisBltyServiceManager mng = new HisBltyServiceManager(param.CommonParam);
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
