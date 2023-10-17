using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAdrMedicineType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAdrMedicineTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAdrMedicineTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAdrMedicineTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ADR_MEDICINE_TYPE>> result = new ApiResultObject<List<HIS_ADR_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisAdrMedicineTypeManager mng = new HisAdrMedicineTypeManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_ADR_MEDICINE_TYPE> result = new ApiResultObject<HIS_ADR_MEDICINE_TYPE>(null);
                if (param != null)
                {
                    HisAdrMedicineTypeManager mng = new HisAdrMedicineTypeManager(param.CommonParam);
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
            ApiResultObject<HIS_ADR_MEDICINE_TYPE> result = null;
            if (param != null)
            {
                HisAdrMedicineTypeManager mng = new HisAdrMedicineTypeManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
