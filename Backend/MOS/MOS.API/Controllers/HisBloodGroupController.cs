using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBloodGroup;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBloodGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBloodGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BLOOD_GROUP>> result = new ApiResultObject<List<HIS_BLOOD_GROUP>>(null);
                if (param != null)
                {
                    HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_BLOOD_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD_GROUP> result = new ApiResultObject<HIS_BLOOD_GROUP>(null);
                if (param != null)
                {
                    HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_BLOOD_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD_GROUP> result = new ApiResultObject<HIS_BLOOD_GROUP>(null);
                if (param != null)
                {
                    HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
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
                    HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_BLOOD_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD_GROUP> result = new ApiResultObject<HIS_BLOOD_GROUP>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
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
            ApiResultObject<HIS_BLOOD_GROUP> result = null;
            if (param != null && param.ApiData != null)
            {
                HisBloodGroupManager mng = new HisBloodGroupManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
