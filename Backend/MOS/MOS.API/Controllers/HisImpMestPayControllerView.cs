using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestPay;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestPayController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestPayViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisImpMestPayViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_PAY>> result = new ApiResultObject<List<V_HIS_IMP_MEST_PAY>>(null);
                if (param != null)
                {
                    HisImpMestPayManager mng = new HisImpMestPayManager(param.CommonParam);
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
