using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisExpMestTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExpMestTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST_TYPE>> result = new ApiResultObject<List<HIS_EXP_MEST_TYPE>>(null);
                if (param != null)
                {
                    HisExpMestTypeManager mng = new HisExpMestTypeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_EXP_MEST_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST_TYPE> result = new ApiResultObject<HIS_EXP_MEST_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestTypeManager mng = new HisExpMestTypeManager(param.CommonParam);
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
