using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientProgram;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPatientProgramController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientProgramFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPatientProgramFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PATIENT_PROGRAM>> result = new ApiResultObject<List<HIS_PATIENT_PROGRAM>>(null);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisPatientProgramViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPatientProgramViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_PROGRAM>> result = new ApiResultObject<List<V_HIS_PATIENT_PROGRAM>>(null);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetViewByCode")]
        public ApiResult Get(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<V_HIS_PATIENT_PROGRAM> result = new ApiResultObject<V_HIS_PATIENT_PROGRAM>(null);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
                    result = mng.GetViewByCode(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_PATIENT_PROGRAM> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PATIENT_PROGRAM> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_PATIENT_PROGRAM> param)
        {
            try
            {
                ApiResultObject<HIS_PATIENT_PROGRAM> result = new ApiResultObject<HIS_PATIENT_PROGRAM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPatientProgramManager mng = new HisPatientProgramManager(param.CommonParam);
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
