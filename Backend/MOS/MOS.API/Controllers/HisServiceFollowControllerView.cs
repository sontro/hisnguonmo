using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceFollow;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceFollowController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceFollowViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisServiceFollowViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_FOLLOW>> result = new ApiResultObject<List<V_HIS_SERVICE_FOLLOW>>(null);
                if (param != null)
                {
                    HisServiceFollowManager mng = new HisServiceFollowManager(param.CommonParam);
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
