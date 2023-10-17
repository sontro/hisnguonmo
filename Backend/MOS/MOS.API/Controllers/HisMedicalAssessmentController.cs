using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicalAssessment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMedicalAssessmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicalAssessmentFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicalAssessmentFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICAL_ASSESSMENT>> result = new ApiResultObject<List<HIS_MEDICAL_ASSESSMENT>>(null);
                if (param != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_MEDICAL_ASSESSMENT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
                if (param != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MEDICAL_ASSESSMENT> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
                if (param != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = new ApiResultObject<HIS_MEDICAL_ASSESSMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
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
		
		[HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_MEDICAL_ASSESSMENT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Save")]
        public ApiResult Save(ApiParam<HisMedicalAssessmentSDO> param)
        {
            try
            {
                ApiResultObject<HisMedicalAssessmentResultSDO> result = new ApiResultObject<HisMedicalAssessmentResultSDO>(null);
                if (param != null)
                {
                    HisMedicalAssessmentManager mng = new HisMedicalAssessmentManager(param.CommonParam);
                    result = mng.Save(param.ApiData);
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
