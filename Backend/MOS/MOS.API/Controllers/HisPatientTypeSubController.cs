using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientTypeSub;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPatientTypeSubController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeSubFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientTypeSubFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_SUB>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_SUB>>(null);
                if (param != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeSubViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPatientTypeSubViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_TYPE_SUB>> result = new ApiResultObject<List<V_HIS_PATIENT_TYPE_SUB>>(null);
                if (param != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_PATIENT_TYPE_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
                if (param != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PATIENT_TYPE_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
                if (param != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_PATIENT_TYPE_SUB> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_PATIENT_TYPE_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_SUB> result = new ApiResultObject<HIS_PATIENT_TYPE_SUB>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientTypeSubManager mng = new HisPatientTypeSubManager(param.CommonParam);
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
