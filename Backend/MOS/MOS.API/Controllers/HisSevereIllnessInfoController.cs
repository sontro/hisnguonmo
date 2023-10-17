using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSevereIllnessInfo;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSevereIllnessInfoController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSevereIllnessInfoFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSevereIllnessInfoFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SEVERE_ILLNESS_INFO>> result = new ApiResultObject<List<HIS_SEVERE_ILLNESS_INFO>>(null);
                if (param != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SEVERE_ILLNESS_INFO> param)
        {
            try
            {
                ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
                if (param != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SEVERE_ILLNESS_INFO> param)
        {
            try
            {
                ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
                if (param != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
                ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = null;
            if (param != null && param.ApiData != null)
            {
                HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateOrUpdate")]
        public ApiResult CreateOrUpdate(ApiParam<SevereIllnessInfoSDO> param)
        {
            try
            {
                ApiResultObject<SevereIllnessInfoSDO> result = new ApiResultObject<SevereIllnessInfoSDO>(null);
                if (param != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
                    result = mng.CreateOrUpdate(param.ApiData);
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
