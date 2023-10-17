using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisAccountBook;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAccountBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("RequestToMe")]
        public ApiResult RequestToMe(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<AuthorityAccountBookSDO>> result = new ApiResultObject<List<AuthorityAccountBookSDO>>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.RequestToMe(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("MyRequest")]
        public ApiResult MyRequest(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<AuthorityAccountBookSDO> result = new ApiResultObject<AuthorityAccountBookSDO>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.MyRequest(param.ApiData);
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
        [ActionName("Request")]
        public ApiResult Request(ApiParam<AuthorityAccountBookSDO> param)
        {
            try
            {
                ApiResultObject<AuthorityAccountBookSDO> result = new ApiResultObject<AuthorityAccountBookSDO>(null);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.Request(param.ApiData);
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
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<ApprovalAccountBookSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                    result = mng.Approve(param.ApiData);
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
        [ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<UnapprovalAccountBookSDO> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null && param.ApiData != null)
            {
                HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                result = mng.Unapprove(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Reject")]
        public ApiResult Reject(ApiParam<RejectAccountBookSDO> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            if (param != null && param.ApiData != null)
            {
                HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
                result = mng.Reject(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Cancel")]
        public ApiResult Cancel(ApiParam<AuthorityAccountBookSDO> param)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            HisAccountBookManager mng = new HisAccountBookManager(param.CommonParam);
            result = mng.Cancel();
            return new ApiResult(result, this.ActionContext);
        }
    }
}
