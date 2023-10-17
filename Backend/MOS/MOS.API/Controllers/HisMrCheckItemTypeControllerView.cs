using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrCheckItemType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMrCheckItemTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMrCheckItemTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMrCheckItemTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MR_CHECK_ITEM_TYPE>> result = new ApiResultObject<List<V_HIS_MR_CHECK_ITEM_TYPE>>(null);
                if (param != null)
                {
                    HisMrCheckItemTypeManager mng = new HisMrCheckItemTypeManager(param.CommonParam);
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
