using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAlert;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAlertController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAlertFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAlertFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ALERT>> result = new ApiResultObject<List<HIS_ALERT>>(null);
                if (param != null)
                {
                    HisAlertManager mng = new HisAlertManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisAlertSDO> param)
        {
            try
            {
                ApiResultObject<HIS_ALERT> result = new ApiResultObject<HIS_ALERT>(null);
                if (param != null)
                {
                    HisAlertManager mng = new HisAlertManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ALERT> param)
        {
            try
            {
                ApiResultObject<HIS_ALERT> result = new ApiResultObject<HIS_ALERT>(null);
                if (param != null)
                {
                    HisAlertManager mng = new HisAlertManager(param.CommonParam);
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
                    HisAlertManager mng = new HisAlertManager(param.CommonParam);
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
                ApiResultObject<HIS_ALERT> result = new ApiResultObject<HIS_ALERT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAlertManager mng = new HisAlertManager(param.CommonParam);
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
            ApiResultObject<HIS_ALERT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisAlertManager mng = new HisAlertManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Receiver")]
        public ApiResult Receiver(ApiParam<long> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null)
            {
                HisAlertManager mng = new HisAlertManager(param.CommonParam);
                result = mng.Receiver(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Reject")]
        public ApiResult Reject(ApiParam<long> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null)
            {
                HisAlertManager mng = new HisAlertManager(param.CommonParam);
                result = mng.Reject(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
