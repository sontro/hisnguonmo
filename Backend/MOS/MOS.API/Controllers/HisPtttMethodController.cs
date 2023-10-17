using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPtttMethod;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPtttMethodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPtttMethodFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPtttMethodFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PTTT_METHOD>> result = new ApiResultObject<List<HIS_PTTT_METHOD>>(null);
                if (param != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_PTTT_METHOD> param)
        {
            try
            {
                ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
                if (param != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PTTT_METHOD> param)
        {
            try
            {
                ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
                if (param != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_PTTT_METHOD> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_PTTT_METHOD> param)
        {
            try
            {
                ApiResultObject<HIS_PTTT_METHOD> result = new ApiResultObject<HIS_PTTT_METHOD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_PTTT_METHOD>> param)
        {
            try
            {
                ApiResultObject<List<HIS_PTTT_METHOD>> result = new ApiResultObject<List<HIS_PTTT_METHOD>>(null);
                if (param != null)
                {
                    HisPtttMethodManager mng = new HisPtttMethodManager(param.CommonParam);
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
    }
}
