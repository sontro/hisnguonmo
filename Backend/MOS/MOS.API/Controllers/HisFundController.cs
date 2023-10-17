using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisFund;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisFundController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisFundFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisFundFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_FUND>> result = new ApiResultObject<List<HIS_FUND>>(null);
                if (param != null)
                {
                    HisFundManager mng = new HisFundManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_FUND> param)
        {
            try
            {
                ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
                if (param != null)
                {
                    HisFundManager mng = new HisFundManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_FUND> param)
        {
            try
            {
                ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
                if (param != null)
                {
                    HisFundManager mng = new HisFundManager(param.CommonParam);
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
                    HisFundManager mng = new HisFundManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_FUND> param)
        {
            try
            {
                ApiResultObject<HIS_FUND> result = new ApiResultObject<HIS_FUND>(null);
                if (param != null && param.ApiData != null)
                {
                    HisFundManager mng = new HisFundManager(param.CommonParam);
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
    }
}
