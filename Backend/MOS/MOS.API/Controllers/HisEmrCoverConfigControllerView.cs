using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmrCoverConfig;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEmrCoverConfigController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEmrCoverConfigViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEmrCoverConfigViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EMR_COVER_CONFIG>> result = new ApiResultObject<List<V_HIS_EMR_COVER_CONFIG>>(null);
                if (param != null)
                {
                    HisEmrCoverConfigManager mng = new HisEmrCoverConfigManager(param.CommonParam);
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
