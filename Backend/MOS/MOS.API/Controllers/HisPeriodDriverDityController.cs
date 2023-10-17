using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPeriodDriverDity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisPeriodDriverDityController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPeriodDriverDityFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPeriodDriverDityFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PERIOD_DRIVER_DITY>> result = new ApiResultObject<List<HIS_PERIOD_DRIVER_DITY>>(null);
                if (param != null)
                {
                    HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_PERIOD_DRIVER_DITY> param)
        {
            try
            {
                ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
                if (param != null)
                {
                    HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PERIOD_DRIVER_DITY> param)
        {
            try
            {
                ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
                if (param != null)
                {
                    HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
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
                    HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
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
                ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = new ApiResultObject<HIS_PERIOD_DRIVER_DITY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
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
            ApiResultObject<HIS_PERIOD_DRIVER_DITY> result = null;
            if (param != null && param.ApiData != null)
            {
                HisPeriodDriverDityManager mng = new HisPeriodDriverDityManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
