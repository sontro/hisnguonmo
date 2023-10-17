using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpBltyService;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpBltyServiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpBltyServiceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisExpBltyServiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_BLTY_SERVICE>> result = new ApiResultObject<List<V_HIS_EXP_BLTY_SERVICE>>(null);
                if (param != null)
                {
                    HisExpBltyServiceManager mng = new HisExpBltyServiceManager(param.CommonParam);
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
