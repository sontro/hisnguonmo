using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrChecklist;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMrChecklistController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMrChecklistViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMrChecklistViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MR_CHECKLIST>> result = new ApiResultObject<List<V_HIS_MR_CHECKLIST>>(null);
                if (param != null)
                {
                    HisMrChecklistManager mng = new HisMrChecklistManager(param.CommonParam);
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
