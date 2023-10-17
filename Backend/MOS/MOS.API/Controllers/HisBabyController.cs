using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBaby;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBabyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBabyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBabyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BABY>> result = new ApiResultObject<List<HIS_BABY>>(null);
                if (param != null)
                {
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisBabySDO> param)
        {
            try
            {
                ApiResultObject<HIS_BABY> result = new ApiResultObject<HIS_BABY>(null);
                if (param != null)
                {
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HisBabySDO> param)
        {
            try
            {
                ApiResultObject<HIS_BABY> result = new ApiResultObject<HIS_BABY>(null);
                if (param != null)
                {
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
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
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
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
                ApiResultObject<HIS_BABY> result = new ApiResultObject<HIS_BABY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
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
            ApiResultObject<HIS_BABY> result = null;
            if (param != null && param.ApiData != null)
            {
                HisBabyManager mng = new HisBabyManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Sync")]
        public ApiResult Sync(ApiParam<List<BabySyncSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisBabyManager mng = new HisBabyManager(param.CommonParam);
                    result = mng.Sync(param.ApiData);
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
