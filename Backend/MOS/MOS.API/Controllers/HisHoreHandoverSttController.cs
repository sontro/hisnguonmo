using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoreHandoverStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisHoreHandoverSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoreHandoverSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHoreHandoverSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HORE_HANDOVER_STT>> result = new ApiResultObject<List<HIS_HORE_HANDOVER_STT>>(null);
                if (param != null)
                {
                    HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_HORE_HANDOVER_STT> param)
        {
            try
            {
                ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
                if (param != null)
                {
                    HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_HORE_HANDOVER_STT> param)
        {
            try
            {
                ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
                if (param != null)
                {
                    HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
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
                    HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
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
                ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
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
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisHoreHandoverSttManager mng = new HisHoreHandoverSttManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
