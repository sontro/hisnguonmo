using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisImpMestTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_TYPE>> result = new ApiResultObject<List<HIS_IMP_MEST_TYPE>>(null);
                if (param != null)
                {
                    HisImpMestTypeManager mng = new HisImpMestTypeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_IMP_MEST_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST_TYPE> result = new ApiResultObject<HIS_IMP_MEST_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisImpMestTypeManager mng = new HisImpMestTypeManager(param.CommonParam);
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
