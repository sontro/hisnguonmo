using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEinvoiceType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisEinvoiceTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEinvoiceTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisEinvoiceTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EINVOICE_TYPE>> result = new ApiResultObject<List<HIS_EINVOICE_TYPE>>(null);
                if (param != null)
                {
                    HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EINVOICE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_EINVOICE_TYPE> result = new ApiResultObject<HIS_EINVOICE_TYPE>(null);
                if (param != null)
                {
                    HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EINVOICE_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_EINVOICE_TYPE> result = new ApiResultObject<HIS_EINVOICE_TYPE>(null);
                if (param != null)
                {
                    HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
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
                    HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
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
                ApiResultObject<HIS_EINVOICE_TYPE> result = new ApiResultObject<HIS_EINVOICE_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
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
            ApiResultObject<HIS_EINVOICE_TYPE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisEinvoiceTypeManager mng = new HisEinvoiceTypeManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
