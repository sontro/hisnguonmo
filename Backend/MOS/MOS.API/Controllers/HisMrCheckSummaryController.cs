using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMrCheckSummary;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMrCheckSummaryController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMrCheckSummaryFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMrCheckSummaryFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MR_CHECK_SUMMARY>> result = new ApiResultObject<List<HIS_MR_CHECK_SUMMARY>>(null);
                if (param != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MR_CHECK_SUMMARY> param)
        {
            try
            {
                ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
                if (param != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MR_CHECK_SUMMARY> param)
        {
            try
            {
                ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
                if (param != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
                ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateOrUpdate")]
        public ApiResult CreateOrUpdate(ApiParam<MrCheckSummarySDO> param)
        {
            try
            {
                ApiResultObject<MrCheckSummarySDO> result = new ApiResultObject<MrCheckSummarySDO>(null);
                if (param != null)
                {
                    HisMrCheckSummaryManager mng = new HisMrCheckSummaryManager(param.CommonParam);
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
