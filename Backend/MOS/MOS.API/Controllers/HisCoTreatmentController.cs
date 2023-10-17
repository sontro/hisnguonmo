using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCoTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisCoTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCoTreatmentFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCoTreatmentFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CO_TREATMENT>> result = new ApiResultObject<List<HIS_CO_TREATMENT>>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisCoTreatmentSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_CO_TREATMENT> param)
        {
            try
            {
                ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
                ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
            ApiResultObject<HIS_CO_TREATMENT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Receive")]
        public ApiResult Receive(ApiParam<HisCoTreatmentReceiveSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
                    result = mng.Receive(param.ApiData);
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
        [ActionName("Finish")]
        public ApiResult Finish(ApiParam<HisCoTreatmentFinishSDO> param)
        {
            try
            {
                ApiResultObject<HIS_CO_TREATMENT> result = new ApiResultObject<HIS_CO_TREATMENT>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
                    result = mng.Finish(param.ApiData);
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
