using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEkipPlanUser;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisEkipPlanUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEkipPlanUserFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisEkipPlanUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EKIP_PLAN_USER>> result = new ApiResultObject<List<HIS_EKIP_PLAN_USER>>(null);
                if (param != null)
                {
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EKIP_PLAN_USER> param)
        {
            try
            {
                ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
                if (param != null)
                {
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EKIP_PLAN_USER> param)
        {
            try
            {
                ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
                if (param != null)
                {
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
                ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
            ApiResultObject<HIS_EKIP_PLAN_USER> result = null;
            if (param != null && param.ApiData != null)
            {
                HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
