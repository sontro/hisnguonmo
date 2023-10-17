using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisRegisterGate;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisRegisterGateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRegisterGateFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]//cho phep chay tren kiosk ko can dang nhap
        public ApiResult Get(ApiParam<HisRegisterGateFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REGISTER_GATE>> result = new ApiResultObject<List<HIS_REGISTER_GATE>>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_REGISTER_GATE> param)
        {
            try
            {
                ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REGISTER_GATE> param)
        {
            try
            {
                ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
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
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
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
                ApiResultObject<HIS_REGISTER_GATE> result = new ApiResultObject<HIS_REGISTER_GATE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
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
            ApiResultObject<HIS_REGISTER_GATE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRegisterGateCurrentNumOrderFilter>), "param")]
        [ActionName("GetCurrentNumOrder")]
        public ApiResult GetCurrentNumOrder(ApiParam<HisRegisterGateCurrentNumOrderFilter> param)
        {
            try
            {
                ApiResultObject<List<HisRegisterGateSDO>> result = new ApiResultObject<List<HisRegisterGateSDO>>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                    result = mng.GetCurrentNumOrder(param.ApiData);
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
        [ActionName("UpdateNumOrder")]
        public ApiResult UpdateNumOrder(ApiParam<List<HisRegisterGateSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                    result = mng.UpdateNumOrder(param.ApiData);
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
        [AllowAnonymous]
        [ActionName("GetDepartment")]
        public ApiResult GetDepartment()
        {
            try
            {
                ApiResultObject<List<RegisterGateDepartmentSDO>> result = new ApiResultObject<List<RegisterGateDepartmentSDO>>(null);
                HisRegisterGateManager mng = new HisRegisterGateManager();
                result = mng.GetDepartment();
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ActionName("IssueOrderNumber")]
        public ApiResult IssueOrderNumber(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<IssueOrderNumberSDO> result = new ApiResultObject<IssueOrderNumberSDO>(null, false);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                    result = mng.IssueOrderNumber(param.ApiData);
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
        [ActionName("Call")]
        public ApiResult Call(ApiParam<RegisterGateCallSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_REGISTER_REQ>> result = new ApiResultObject<List<HIS_REGISTER_REQ>>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                    result = mng.Call(param.ApiData);
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
        [ActionName("ReCall")]
        public ApiResult ReCall(ApiParam<RegisterGateCallSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_REGISTER_REQ>> result = new ApiResultObject<List<HIS_REGISTER_REQ>>(null);
                if (param != null)
                {
                    HisRegisterGateManager mng = new HisRegisterGateManager(param.CommonParam);
                    result = mng.ReCall(param.ApiData);
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
