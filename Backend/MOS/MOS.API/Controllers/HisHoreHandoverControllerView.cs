using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoreHandover;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisHoreHandoverController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoreHandoverViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisHoreHandoverViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HORE_HANDOVER>> result = new ApiResultObject<List<V_HIS_HORE_HANDOVER>>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
