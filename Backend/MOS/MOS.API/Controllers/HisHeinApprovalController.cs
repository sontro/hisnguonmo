using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHeinApproval;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisHeinApprovalController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHeinApprovalFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisHeinApprovalFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_HEIN_APPROVAL>> result = new ApiResultObject<List<HIS_HEIN_APPROVAL>>(null);
                if (param != null)
                {
                    HisHeinApprovalManager mng = new HisHeinApprovalManager(param.CommonParam);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<HIS_HEIN_APPROVAL> param)
        {
            try
            {
                ApiResultObject<HIS_HEIN_APPROVAL> result = new ApiResultObject<HIS_HEIN_APPROVAL>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHeinApprovalManager mng = new HisHeinApprovalManager(param.CommonParam);
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
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_HEIN_APPROVAL> param)
        {
            try
            {
                ApiResultObject<V_HIS_HEIN_APPROVAL> result = new ApiResultObject<V_HIS_HEIN_APPROVAL>(null);
                if (param != null && param.ApiData != null)
                {
                    HisHeinApprovalManager mng = new HisHeinApprovalManager(param.CommonParam);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisHeinApprovalManager mng = new HisHeinApprovalManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHeinApprovalViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisHeinApprovalViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HEIN_APPROVAL>> result = new ApiResultObject<List<V_HIS_HEIN_APPROVAL>>(null);
                if (param != null)
                {
                    HisHeinApprovalManager mng = new HisHeinApprovalManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
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
