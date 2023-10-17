using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAllergyCard;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAllergyCardController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAllergyCardFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAllergyCardFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ALLERGY_CARD>> result = new ApiResultObject<List<HIS_ALLERGY_CARD>>(null);
                if (param != null)
                {
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisAllergyCardSDO> param)
        {
            try
            {
                ApiResultObject<HisAllergyCardResultSDO> result = new ApiResultObject<HisAllergyCardResultSDO>(null);
                if (param != null)
                {
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisAllergyCardSDO> param)
        {
            try
            {
                ApiResultObject<HisAllergyCardResultSDO> result = new ApiResultObject<HisAllergyCardResultSDO>(null);
                if (param != null)
                {
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
                ApiResultObject<HIS_ALLERGY_CARD> result = new ApiResultObject<HIS_ALLERGY_CARD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
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
            ApiResultObject<HIS_ALLERGY_CARD> result = null;
            if (param != null && param.ApiData != null)
            {
                HisAllergyCardManager mng = new HisAllergyCardManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
