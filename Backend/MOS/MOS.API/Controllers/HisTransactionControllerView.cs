using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransaction;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTransactionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionView5FilterQuery>), "param")]
        [ActionName("GetView5")]
        public ApiResult GetView5(ApiParam<HisTransactionView5FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TRANSACTION_5>> result = null;
                if (param != null)
                {
                    HisTransactionManager mng = new HisTransactionManager(param.CommonParam);
                    result = mng.GetView5(param.ApiData);
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
