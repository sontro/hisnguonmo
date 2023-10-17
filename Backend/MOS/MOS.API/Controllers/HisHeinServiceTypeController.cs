using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHeinServiceType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisHeinServiceTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHeinServiceTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHeinServiceTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HEIN_SERVICE_TYPE>> result = new ApiResultObject<List<HIS_HEIN_SERVICE_TYPE>>(null);
                if (param != null)
                {
                    HisHeinServiceTypeManager mng = new HisHeinServiceTypeManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_HEIN_SERVICE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_HEIN_SERVICE_TYPE> result = new ApiResultObject<HIS_HEIN_SERVICE_TYPE>(null);
                if (param != null)
                {
                    HisHeinServiceTypeManager mng = new HisHeinServiceTypeManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
