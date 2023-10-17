using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisTreatmentTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisTreatmentTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_TREATMENT_TYPE>> result = new ApiResultObject<List<HIS_TREATMENT_TYPE>>(null);
                if (param != null)
                {
                    HisTreatmentTypeManager mng = new HisTreatmentTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_TREATMENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_TYPE> result = new ApiResultObject<HIS_TREATMENT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentTypeManager mng = new HisTreatmentTypeManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_TREATMENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_TREATMENT_TYPE> result = new ApiResultObject<HIS_TREATMENT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisTreatmentTypeManager mng = new HisTreatmentTypeManager(param.CommonParam);
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
