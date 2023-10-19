using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsOtp;
using ACS.MANAGER.Manager;
using ACS.SDO;
using Inventec.Backend.MANAGER;
using Inventec.Common.Logging;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsOtpController : BaseApiController
    {
        [HttpPost]
        [ActionName("Required")]
        [AllowAnonymous]
        public ApiResult Required(ApiParam<OtpRequiredSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.Required(param.ApiData);
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
        [ActionName("RequiredForLogin")]
        [AllowAnonymous]
        public ApiResult RequiredForLogin(ApiParam<OtpRequiredForLoginSDO> param)
        {
            try
            {
                ApiResultObject<OtpRequiredForLoginResultSDO> result = new ApiResultObject<OtpRequiredForLoginResultSDO>(null);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.RequiredForLogin(param.ApiData);
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
        [ActionName("RequiredOnly")]
        [AllowAnonymous]
        public ApiResult RequiredOnly(ApiParam<OtpRequiredOnlySDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.RequiredOnly(param.ApiData);
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
        [ActionName("RequiredOnlyWithTemplateCode")]
        [AllowAnonymous]
        public ApiResult RequiredOnlyWithTemplateCode(ApiParam<OtpRequiredOnlyWithTemplateCodeSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.RequiredOnlyWithTemplateCode(param.ApiData);
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
        [ActionName("RequiredWithMessage")]
        [AllowAnonymous]
        public ApiResult RequiredWithMessage(ApiParam<OtpRequiredWithMessageSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.RequiredWithMessage(param.ApiData);
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
        [ActionName("RequiredOnlyWithMessage")]
        [AllowAnonymous]
        public ApiResult RequiredOnlyWithMessage(ApiParam<OtpRequiredOnlyWithMessageSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.RequiredOnlyWithMessage(param.ApiData);
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
        [ActionName("Verify")]
        [AllowAnonymous]
        public ApiResult Verify(ApiParam<OtpVerifySDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.Verify(param.ApiData);
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
        [ActionName("VerifyForLogin")]
        [AllowAnonymous]
        public ApiResult VerifyForLogin(ApiParam<OtpVerifyForLoginSDO> param)
        {
            try
            {
                ApiResultObject<Inventec.Token.Core.TokenData> result = new ApiResultObject<Inventec.Token.Core.TokenData>(null);
                if (param != null && param.ApiData != null)
                {
                    param.ApiData.LoginAddress = GetAddress(this.ActionContext);
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.VerifyForLogin(param.ApiData);
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
        public ApiResult CreateList(ApiParam<List<ACS_OTP>> param)
        {
            try
            {
                ApiResultObject<List<ACS_OTP>> result = new ApiResultObject<List<ACS_OTP>>(null, false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<ACS_OTP>> param)
        {
            try
            {
                ApiResultObject<List<ACS_OTP>> result = new ApiResultObject<List<ACS_OTP>>(null, false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.UpdateList(param.ApiData);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<AcsOtpFilterQuery>), "param")]
        [ActionName("GetByPhone")]
        [AllowAnonymous]
        public ApiResult GetByPhone(ApiParam<AcsOtpFilterQuery> param)
        {
            try
            {
                ApiResultObject<ACS_OTP> result = new ApiResultObject<ACS_OTP>(null);
                if (param != null)
                {
                    AcsOtpManager mng = new AcsOtpManager(param.CommonParam);
                    result = mng.GetByPhone(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
