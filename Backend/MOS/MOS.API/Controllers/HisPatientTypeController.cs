using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPatientTypeController : BaseApiController
    {
        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE>> result = new ApiResultObject<List<HIS_PATIENT_TYPE>>(null);
                if (param != null)
                {
                    HisPatientTypeManager mng = new HisPatientTypeManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE> result = new ApiResultObject<HIS_PATIENT_TYPE>(null);
                if (param != null)
                {
                    HisPatientTypeManager mng = new HisPatientTypeManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE> result = new ApiResultObject<HIS_PATIENT_TYPE>(null);
                if (param != null)
                {
                    HisPatientTypeManager mng = new HisPatientTypeManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPatientTypeManager mng = new HisPatientTypeManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_PATIENT_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE> result = new ApiResultObject<HIS_PATIENT_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientTypeManager mng = new HisPatientTypeManager(param.CommonParam);
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
