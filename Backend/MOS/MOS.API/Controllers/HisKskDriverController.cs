using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisKskDriver;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisKskDriverController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisKskDriverFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisKskDriverFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_KSK_DRIVER>> result = new ApiResultObject<List<HIS_KSK_DRIVER>>(null);
                if (param != null)
                {
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisKskDriverSDO> param)
        {
            try
            {
                ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
                if (param != null)
                {
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisKskDriverSDO> param)
        {
            try
            {
                ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
                if (param != null)
                {
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
                ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
                if (param != null && param.ApiData != null)
                {
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
            ApiResultObject<HIS_KSK_DRIVER> result = null;
            if (param != null && param.ApiData != null)
            {
                HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Sync")]
        public ApiResult Sync(ApiParam<List<KskDriverSyncSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisKskDriverManager mng = new HisKskDriverManager(param.CommonParam);
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
