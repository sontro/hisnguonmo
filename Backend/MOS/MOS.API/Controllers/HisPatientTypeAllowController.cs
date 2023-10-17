using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisPatientTypeAllow;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPatientTypeAllowController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeAllowFilterQuery>), "param")]
        [ActionName("Get")]

        public ApiResult Get(ApiParam<HisPatientTypeAllowFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_ALLOW>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ALLOW>>(null);
                if (param != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeAllowViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisPatientTypeAllowViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_TYPE_ALLOW>> result = new ApiResultObject<List<V_HIS_PATIENT_TYPE_ALLOW>>(null);
                if (param != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_PATIENT_TYPE_ALLOW> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
                if (param != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_PATIENT_TYPE_ALLOW> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
                if (param != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_PATIENT_TYPE_ALLOW> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_PATIENT_TYPE_ALLOW> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ALLOW> result = new ApiResultObject<HIS_PATIENT_TYPE_ALLOW>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientTypeAllowManager mng = new HisPatientTypeAllowManager(param.CommonParam);
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
