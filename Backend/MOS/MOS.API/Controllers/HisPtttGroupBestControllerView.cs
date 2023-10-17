using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPtttGroupBest;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPtttGroupBestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPtttGroupBestViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPtttGroupBestViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PTTT_GROUP_BEST>> result = new ApiResultObject<List<V_HIS_PTTT_GROUP_BEST>>(null);
                if (param != null)
                {
                    HisPtttGroupBestManager mng = new HisPtttGroupBestManager(param.CommonParam);
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
