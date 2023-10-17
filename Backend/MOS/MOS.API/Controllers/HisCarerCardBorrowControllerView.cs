using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCarerCardBorrow;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisCarerCardBorrowController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCarerCardBorrowViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisCarerCardBorrowViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CARER_CARD_BORROW>> result = new ApiResultObject<List<V_HIS_CARER_CARD_BORROW>>(null);
                if (param != null)
                {
                    HisCarerCardBorrowManager mng = new HisCarerCardBorrowManager(param.CommonParam);
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
