using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.Token;
using ACS.SDO;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token;
using Inventec.Token.AuthSystem;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ACS.API.Controllers
{
    public class TokenController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ActionName("Login")]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ApiResult Login()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<Inventec.Token.Core.TokenData> result = mng.Login(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("Renew")]
        [AllowAnonymous]
        public ApiResult Renew()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<Inventec.Token.Core.TokenData> result = mng.Renew(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetAuthenticated")]
        [AllowAnonymous]
        public ApiResult GetAuthenticated()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<Inventec.Token.Core.TokenData> result = mng.GetAuthenticated(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetAuthenticatedByAddress")]
        [AllowAnonymous]
        public ApiResult GetAuthenticatedByAddress()
        {
            try
            {
                Inventec.Common.Logging.LogSystem.Debug("GetAuthenticatedByAddress => begin...");
                TokenManager mng = new TokenManager();
                ApiResultObject<Inventec.Token.Core.TokenData> result = mng.GetAuthenticatedByAddress(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ActionName("GetCredentialData")]
        public ApiResult GetCredentialData()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<Inventec.Token.Core.CredentialData> result = mng.GetCredentialData(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("SetCredentialData")]
        public ApiResult SetCredentialData(Inventec.Token.Core.CredentialData data)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (data != null)
                {
                    TokenManager mng = new TokenManager();
                    result = mng.SetCredentialData(this.ActionContext, data, new CommonParam());
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
        public ApiResult ChangePassword()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<bool> result = mng.ChangePassword(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Logout")]
        public ApiResult Logout()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<bool> result = mng.Logout(this.ActionContext);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("SetLanguage")]
        public ApiResult SetLanguage(ApiParam<string> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<bool> result = mng.SetLanguage(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetCredentialTracking")]
        public ApiResult GetCredentialTracking(ApiParam<string> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<List<AcsCredentialTrackingSDO>> result = new ApiResultObject<List<AcsCredentialTrackingSDO>>(null, false);
                List<AcsCredentialTrackingSDO> resultData = mng.GetTokenDataAlives(param.ApiData);
                result = PackResult<List<AcsCredentialTrackingSDO>>(resultData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<AcsCredentialTrackingSDO>), "param")]
        [ActionName("GetCredentialTrackingUser")]
        public ApiResult GetCredentialTrackingUser(ApiParam<AcsCredentialTrackingSDO> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<AcsCredentialTrackingSDO> result = new ApiResultObject<AcsCredentialTrackingSDO>(null, false);
                AcsCredentialTrackingSDO resultData = mng.GetTokenDataAlivesUser(param.ApiData);
                result = PackResult<AcsCredentialTrackingSDO>(resultData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("SetCredentialAccessTime")]
        public ApiResult SetCredentialAccessTime(List<Inventec.Token.Core.TokenData> tokens)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (tokens != null)
                {
                    TokenManager mng = new TokenManager();
                    result = mng.SetCredentialAlive(this.ActionContext, tokens, new CommonParam());
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
        [ActionName("CloneTokenWithApp")]
        public ApiResult CloneTokenWithApp()
        {
            try
            {
                ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null, false);

                TokenManager mng = new TokenManager();
                result = mng.CloneTokenWithApp(this.ActionContext);

                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("RemoveOtherSession")]
        public ApiResult RemoveOtherSession()
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<bool> result = mng.RemoveOtherSession(this.ActionContext);
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
