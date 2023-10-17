using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRegisterReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisRegisterReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRegisterReqFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]//cho phep chay tren kiosk ko can dang nhap
        public ApiResult Get(ApiParam<HisRegisterReqFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REGISTER_REQ>> result = new ApiResultObject<List<HIS_REGISTER_REQ>>(null);
                if (param != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
        [AllowAnonymous]//cho phep chay tren kiosk ko can dang nhap
        public ApiResult Create(ApiParam<HIS_REGISTER_REQ> param)
        {
            try
            {
                ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
                if (param != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REGISTER_REQ> param)
        {
            try
            {
                ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
                if (param != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
                ApiResultObject<HIS_REGISTER_REQ> result = new ApiResultObject<HIS_REGISTER_REQ>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
            ApiResultObject<HIS_REGISTER_REQ> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<HisRegisterReqSDO> param)
        {
            try
            {
                ApiResultObject<V_HIS_REGISTER_REQ> result = new ApiResultObject<V_HIS_REGISTER_REQ>(null);
                if (param != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
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
        [ActionName("CallPatient")]
        public ApiResult CallPatient(ApiParam<CallPatientSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRegisterReqManager mng = new HisRegisterReqManager(param.CommonParam);
                    result = mng.CallPatient(param.ApiData);
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
