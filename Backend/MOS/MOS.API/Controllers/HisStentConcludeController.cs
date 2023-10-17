using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisStentConclude;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisStentConcludeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisStentConcludeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisStentConcludeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<HIS_STENT_CONCLUDE>>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_STENT_CONCLUDE> param)
        {
            try
            {
                ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_STENT_CONCLUDE> param)
        {
            try
            {
                ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_STENT_CONCLUDE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<HIS_STENT_CONCLUDE>>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<HIS_STENT_CONCLUDE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_STENT_CONCLUDE>> result = new ApiResultObject<List<HIS_STENT_CONCLUDE>>(null);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<HIS_STENT_CONCLUDE>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
                ApiResultObject<HIS_STENT_CONCLUDE> result = new ApiResultObject<HIS_STENT_CONCLUDE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
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
            ApiResultObject<HIS_STENT_CONCLUDE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisStentConcludeManager mng = new HisStentConcludeManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
