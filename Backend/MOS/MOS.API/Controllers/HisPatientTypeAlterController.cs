using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisPatientTypeAlter;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPatientTypeAlterController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeAlterFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientTypeAlterFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeAlterViewAppliedFilter>), "param")]
        [ActionName("GetApplied")]
        public ApiResult GetApplied(ApiParam<HisPatientTypeAlterViewAppliedFilter> param)
        {
            try
            {
                ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> result = new ApiResultObject<V_HIS_PATIENT_TYPE_ALTER>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
                    result = mng.GetApplied(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetDistinct")]
        public ApiResult GetDistinct(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>> result = new ApiResultObject<List<HIS_PATIENT_TYPE_ALTER>>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
                    result = mng.GetDistinct(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeAlterViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisPatientTypeAlterViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_TYPE_ALTER>> result = new ApiResultObject<List<V_HIS_PATIENT_TYPE_ALTER>>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetLastByTreatmentId")]
        public ApiResult GetLastByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ALTER> result = new ApiResultObject<HIS_PATIENT_TYPE_ALTER>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
                    result = mng.GetLastByTreatmentId(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewLastByTreatmentId")]
        public ApiResult GetViewLastByTreatmentId(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<V_HIS_PATIENT_TYPE_ALTER> result = new ApiResultObject<V_HIS_PATIENT_TYPE_ALTER>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
                    result = mng.GetViewLastByTreatmentId(param.ApiData);
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
        public ApiResult Create(ApiParam<HisPatientTypeAlterAndTranPatiSDO> param)
        {
            try
            {
                ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> result = new ApiResultObject<HisPatientTypeAlterAndTranPatiSDO>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisPatientTypeAlterAndTranPatiSDO> param)
        {
            try
            {
                ApiResultObject<HisPatientTypeAlterAndTranPatiSDO> result = new ApiResultObject<HisPatientTypeAlterAndTranPatiSDO>(null);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<DeletePatientTypeAlterSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_PATIENT_TYPE_ALTER> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_TYPE_ALTER> result = new ApiResultObject<HIS_PATIENT_TYPE_ALTER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientTypeAlterManager mng = new HisPatientTypeAlterManager(param.CommonParam);
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
