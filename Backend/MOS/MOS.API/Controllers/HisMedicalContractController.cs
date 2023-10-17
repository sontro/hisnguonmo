using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicalContract;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMedicalContractController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicalContractFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicalContractFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICAL_CONTRACT>> result = new ApiResultObject<List<HIS_MEDICAL_CONTRACT>>(null);
                if (param != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
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
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<HisMedicalContractSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
                if (param != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
                    result = mng.CreateSdo(param.ApiData);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisMedicalContractSDO> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
                if (param != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
                    result = mng.UpdateSdo(param.ApiData);
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
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
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
                ApiResultObject<HIS_MEDICAL_CONTRACT> result = new ApiResultObject<HIS_MEDICAL_CONTRACT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
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
            ApiResultObject<HIS_MEDICAL_CONTRACT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Import")]
        public ApiResult Import(ApiParam<List<HIS_MEDICAL_CONTRACT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICAL_CONTRACT>> result = new ApiResultObject<List<HIS_MEDICAL_CONTRACT>>(null);
                if (param != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
                    result = mng.Import(param.ApiData);
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
