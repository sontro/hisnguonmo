using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCarerCard;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisCarerCardController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCarerCardViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisCarerCardViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CARER_CARD>> result = new ApiResultObject<List<V_HIS_CARER_CARD>>(null);
                if (param != null)
                {
                    HisCarerCardManager mng = new HisCarerCardManager(param.CommonParam);
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
