using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentResult;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisTreatmentResultController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentResultFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTreatmentResultFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT_RESULT>> result = new ApiResultObject<List<HIS_TREATMENT_RESULT>>(null);
                if (param != null)
                {
                    HisTreatmentResultManager mng = new HisTreatmentResultManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_TREATMENT_RESULT> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_RESULT> result = new ApiResultObject<HIS_TREATMENT_RESULT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentResultManager mng = new HisTreatmentResultManager(param.CommonParam);
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
