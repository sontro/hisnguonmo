using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsOtpTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsOtpType.Get.AcsOtpTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<ACS.MANAGER.Core.AcsOtpType.Get.AcsOtpTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<ACS_OTP_TYPE>> result = new ApiResultObject<List<ACS_OTP_TYPE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsOtpTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_OTP_TYPE> resultData = managerContainer.Run<List<ACS_OTP_TYPE>>();
                    result = PackResult<List<ACS_OTP_TYPE>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<ACS_OTP_TYPE> param)
        {
            try
            {
                ApiResultObject<ACS_OTP_TYPE> result = new ApiResultObject<ACS_OTP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsOtpTypeManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_OTP_TYPE resultData = managerContainer.Run<ACS_OTP_TYPE>();
                    result = PackResult<ACS_OTP_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<ACS_OTP_TYPE> param)
        {
            try
            {
                ApiResultObject<ACS_OTP_TYPE> result = new ApiResultObject<ACS_OTP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsOtpTypeManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_OTP_TYPE resultData = managerContainer.Run<ACS_OTP_TYPE>();
                    result = PackResult<ACS_OTP_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<ACS_OTP_TYPE> param)
        {
            try
            {
                ApiResultObject<ACS_OTP_TYPE> result = new ApiResultObject<ACS_OTP_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsOtpTypeManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_OTP_TYPE resultData = managerContainer.Run<ACS_OTP_TYPE>();
                    result = PackResult<ACS_OTP_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ACS_OTP_TYPE data = new ACS_OTP_TYPE();
                    data.ID = param.ApiData;
                    Inventec.Backend.MANAGER.ManagerContainer managerContainer = new Inventec.Backend.MANAGER.ManagerContainer(typeof(AcsOtpTypeManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
                    bool resultData = managerContainer.Run<bool>();
                    result = PackResult<bool>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
