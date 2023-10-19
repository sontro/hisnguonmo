using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsToken;
using ACS.MANAGER.Manager;
using Inventec.Backend.MANAGER;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ACS.API.Controllers
{
    public partial class AcsTokenController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<ACS.SDO.AcsTokenLoginSDO>), "param")]
        [ActionName("Login")]
        [AllowAnonymous]
        public ApiResult Login(ApiParam<ACS.SDO.AcsTokenLoginSDO> param)
        {
            try
            {
                ApiResultObject<ACS_USER> result = new ApiResultObject<ACS_USER>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "Login", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS_USER resultData = managerContainer.Run<ACS_USER>();
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
        [ActionName("LoginById")]
        [AllowAnonymous]
        public ApiResult LoginById(ApiParam<ACS.SDO.LoginBySecretKeySDO> param)
        {
            try
            {
                ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    param.ApiData.LOGIN_ADDRESS = GetAddress(this.ActionContext);
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "LoginBySecretKey", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    Inventec.Token.Core.TokenData resultData = managerContainer.Run<Inventec.Token.Core.TokenData>();
                    result = PackResult<Inventec.Token.Core.TokenData>(resultData);
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
        [ActionName("LoginByAuthenRequest")]
        [AllowAnonymous]
        public ApiResult LoginByAuthenRequest(ApiParam<ACS.SDO.LoginByAuthenRequestTDO> param)
        {
            try
            {
                ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    param.ApiData.LOGIN_ADDRESS = GetAddress(this.ActionContext);
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "LoginByAuthenRequest", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    Inventec.Token.Core.TokenData resultData = managerContainer.Run<Inventec.Token.Core.TokenData>();
                    result = PackResult<Inventec.Token.Core.TokenData>(resultData);
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
        [ActionName("LoginByEmail")]
        [AllowAnonymous]
        public ApiResult LoginByEmail(ApiParam<ACS.SDO.LoginByEmailTDO> param)
        {
            try
            {
                ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    param.ApiData.LOGIN_ADDRESS = GetAddress(this.ActionContext);
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "LoginByEmail", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    Inventec.Token.Core.TokenData resultData = managerContainer.Run<Inventec.Token.Core.TokenData>();
                    result = PackResult<Inventec.Token.Core.TokenData>(resultData);
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
        [ApiParamFilter(typeof(ApiParam<ACS.SDO.AcsTokenLoginSDO>), "param")]
        [ActionName("Authorize")]
        [AllowAnonymous]
        public ApiResult Authorize(ApiParam<ACS.SDO.AcsTokenLoginSDO> param)
        {
            try
            {
                ApiResultObject<ACS.SDO.AcsAuthorizeSDO> result = new ApiResultObject<ACS.SDO.AcsAuthorizeSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "Authorize", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    ACS.SDO.AcsAuthorizeSDO resultData = managerContainer.Run<ACS.SDO.AcsAuthorizeSDO>();
                    result = PackResult<ACS.SDO.AcsAuthorizeSDO>(resultData);
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
        [ActionName("SyncToken")]
        [AllowAnonymous]
        public ApiResult SyncToken(ApiParam<List<ACS.SDO.AcsCredentialTrackingSDO>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "SyncToken", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("SyncTokenInsert")]
        [AllowAnonymous]
        public ApiResult SyncTokenInsert(ApiParam<ACS.SDO.AcsTokenSyncInsertSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "SyncToken", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("SyncTokenDelete")]
        [AllowAnonymous]
        public ApiResult SyncTokenDelete(ApiParam<ACS.SDO.AcsTokenSyncDeleteSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManagerExtra), "SyncToken", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
        [ActionName("UpdateExpireTime")]
        public ApiResult UpdateExpireTime(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;

                    ManagerContainer managerContainer = new ManagerContainer(typeof(AcsTokenManager), "UpdateExpireTime", new object[] { param.CommonParam }, new object[] { param.ApiData });
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

        private string GetAddress(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            string address = "";
            try
            {
                var myRequest = ((System.Web.HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request;
                address = myRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (!string.IsNullOrEmpty(address))
                {
                    string[] ipRange = address.Split(',');
                    int le = ipRange.Length - 1;
                    address = ipRange[le];
                }
                else
                {
                    address = myRequest.ServerVariables["REMOTE_ADDR"];
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
           
            return address;
        }
    }
}
