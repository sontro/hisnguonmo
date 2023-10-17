using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrCheckItem;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMrCheckItemController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMrCheckItemViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMrCheckItemViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MR_CHECK_ITEM>> result = new ApiResultObject<List<V_HIS_MR_CHECK_ITEM>>(null);
                if (param != null)
                {
                    HisMrCheckItemManager mng = new HisMrCheckItemManager(param.CommonParam);
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
