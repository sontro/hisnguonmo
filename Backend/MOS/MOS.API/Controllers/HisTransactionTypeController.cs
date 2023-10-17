using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTransactionType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisTransactionTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTransactionTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTransactionTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TRANSACTION_TYPE>> result = new ApiResultObject<List<HIS_TRANSACTION_TYPE>>(null);
                if (param != null)
                {
                    HisTransactionTypeManager mng = new HisTransactionTypeManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_TRANSACTION_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_TRANSACTION_TYPE> result = new ApiResultObject<HIS_TRANSACTION_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTransactionTypeManager mng = new HisTransactionTypeManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
