using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Manager;
using ACS.SDO;
using Inventec.Backend.MANAGER;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.MANAGER.Core.AcsUser.Get.AcsUserFilterQuery>), "param")]
        [ActionName("GetDynamic")]
        public ApiResult GetDynamic(ApiParam<ACS.MANAGER.Core.AcsUser.Get.AcsUserFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<object>> result = new ApiResultObject<List<object>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    AcsUserManager manager = new MANAGER.Manager.AcsUserManager(param.CommonParam);
                    List<object> resultData = manager.GetDynamic(param.ApiData);
                    result = PackResult<List<object>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetByLoginName")]
        [AllowAnonymous]
        public ApiResult GetByLoginName(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<ACS_USER> result = new ApiResultObject<ACS_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_USER resultData = managerContainer.Run<ACS_USER>();
                    if (resultData != null)
                    {
                        resultData.PASSWORD = null;
                        resultData.ACS_ROLE_USER = null;
                    }
                    result = PackResult<ACS_USER>(resultData);
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
        [ActionName("ChangePassword")]
        [AllowAnonymous]
        public ApiResult ChangePassword(ApiParam<ACS.SDO.AcsUserChangePasswordSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ChangePassword", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("ChangePasswordWithOtp")]
        [AllowAnonymous]
        public ApiResult ChangePasswordWithOtp(ApiParam<ACS.SDO.AcsUserChangePasswordWithOtpSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ChangePasswordWithOtp", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("ResetPassword")]
        [AllowAnonymous]
        public ApiResult ResetPassword(ApiParam<ACS_USER> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ResetPassword", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("CheckResetPassword")]
        [AllowAnonymous]
        public ApiResult CheckResetPassword(ApiParam<AcsUserCheckResetPasswordTDO> param)
        {
            try
            {
                ApiResultObject<AcsCheckResetPasswordResultTDO> result = new ApiResultObject<AcsCheckResetPasswordResultTDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "CheckResetPassword", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    AcsCheckResetPasswordResultTDO resultData = managerContainer.Run<AcsCheckResetPasswordResultTDO>();
                    result = PackResult<AcsCheckResetPasswordResultTDO>(resultData);
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
        [ActionName("ResetPasswordB")]
        [AllowAnonymous]
        public ApiResult ResetPasswordB(ApiParam<AcsUserResetPasswordTDO> param)
        {
            try
            {
                ApiResultObject<AcsCheckResetPasswordResultTDO> result = new ApiResultObject<AcsCheckResetPasswordResultTDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ResetPasswordB", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    AcsCheckResetPasswordResultTDO resultData = managerContainer.Run<AcsCheckResetPasswordResultTDO>();
                    result = PackResult<AcsCheckResetPasswordResultTDO>(resultData);
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
        [ActionName("CreateAndGrant")]
        [AllowAnonymous]
        public ApiResult CreateAndGrant(ApiParam<ACS.SDO.CreateAndGrantUserSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "CreateSdo", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("CreateAndGrantList")]
        [AllowAnonymous]
        public ApiResult CreateAndGrantList(ApiParam<List<ACS.SDO.CreateAndGrantUserSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "CreateSdo", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        
        [HttpPost]
        [ActionName("CopyRole")]
        public ApiResult CopyRole(ApiParam<UserRoleCopySDO> param)
        {
            try
            {
                ApiResultObject<List<ACS_ROLE_USER>> result = new ApiResultObject<List<ACS_ROLE_USER>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "CopyRole", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<ACS_ROLE_USER> resultData = managerContainer.Run<List<ACS_ROLE_USER>>();
                    result = PackResult<List<ACS_ROLE_USER>>(resultData);
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
        [ActionName("CheckActive")]
        [AllowAnonymous]
        public ApiResult CheckActive(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "CheckActive", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("ActivationRequired")]
        [AllowAnonymous]
        public ApiResult ActivationRequired(ApiParam<AcsUserActivationRequiredSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ActivationRequired", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("ActivationRequiredWithMessage")]
        [AllowAnonymous]
        public ApiResult ActivationRequiredWithMessage(ApiParam<AcsUserActivationRequiredWithMessageSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "ActivationRequiredWithMessage", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("Activate")]
        [AllowAnonymous]
        public ApiResult Activate(ApiParam<AcsUserActivateSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "Activate", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("InActive")]
        [AllowAnonymous]
        public ApiResult InActive(ApiParam<AcsUserActivationRequiredSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "InActive", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        [HttpPost]
        [ActionName("UpdateListInfo")]
        [AllowAnonymous]
        public ApiResult UpdateListInfo(ApiParam<List<ACS.SDO.AcsUserUpdateInfoSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsUserManager), "UpdateListInfo", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
