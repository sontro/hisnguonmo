using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoreHandover;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisHoreHandoverController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoreHandoverFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHoreHandoverFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HORE_HANDOVER>> result = new ApiResultObject<List<HIS_HORE_HANDOVER>>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisHoreHandoverCreateSDO> param)
        {
            try
            {
                ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisHoreHandoverCreateSDO> param)
        {
            try
            {
                ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HisHoreHandoverSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
                ApiResultObject<HIS_HORE_HANDOVER> result = new ApiResultObject<HIS_HORE_HANDOVER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
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
            ApiResultObject<HIS_HORE_HANDOVER> result = null;
            if (param != null && param.ApiData != null)
            {
                HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Receive")]
        public ApiResult Receive(ApiParam<HisHoreHandoverSDO> param)
        {
            try
            {
                ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
                    result = mng.Receive(param.ApiData);
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
        [ActionName("Unreceive")]
        public ApiResult Unreceive(ApiParam<HisHoreHandoverSDO> param)
        {
            try
            {
                ApiResultObject<HisHoreHandoverResultSDO> result = new ApiResultObject<HisHoreHandoverResultSDO>(null);
                if (param != null)
                {
                    HisHoreHandoverManager mng = new HisHoreHandoverManager(param.CommonParam);
                    result = mng.Unreceive(param.ApiData);
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
