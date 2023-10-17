using SDA.API.Base;
using SDA.MANAGER.Token;
using Inventec.Common.Logging;
using Inventec.Core;
using Inventec.Token;
using System;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public class TokenController : BaseApiController
    {
        [HttpPost]
        [ActionName("UpdateLanguage")]
        public ApiResult UpdateLanguage(ApiParam<string> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<bool> result = mng.UpdateLanguage(param.ApiData);
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
        [ActionName("GetLanguage")]
        [AllowAnonymous]
        public ApiResult GetLanguage(ApiParam<string> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<string> result = mng.GetLanguage();
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
