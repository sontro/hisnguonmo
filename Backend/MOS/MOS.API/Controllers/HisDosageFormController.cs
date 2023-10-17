using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDosageForm;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDosageFormController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDosageFormFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDosageFormFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DOSAGE_FORM>> result = new ApiResultObject<List<HIS_DOSAGE_FORM>>(null);
                if (param != null)
                {
                    HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_DOSAGE_FORM> param)
        {
            try
            {
                ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
                if (param != null)
                {
                    HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_DOSAGE_FORM> param)
        {
            try
            {
                ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
                if (param != null)
                {
                    HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
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
                    HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
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
                ApiResultObject<HIS_DOSAGE_FORM> result = new ApiResultObject<HIS_DOSAGE_FORM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
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
            ApiResultObject<HIS_DOSAGE_FORM> result = null;
            if (param != null && param.ApiData != null)
            {
                HisDosageFormManager mng = new HisDosageFormManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
