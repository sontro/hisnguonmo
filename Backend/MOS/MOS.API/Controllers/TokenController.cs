using Inventec.Common.Logging;
using Inventec.Token.Core;
using Inventec.Core;
using MOS.API.Base;
using MOS.MANAGER.Token;
using MOS.SDO;
using System;
using System.Web.Http;
using System.Collections.Generic;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class TokenController : BaseApiController
    {
        [HttpPost]
        [ActionName("UpdateWorkPlaceList")]
        public ApiResult UpdateWorkPlaceList(ApiParam<List<long>> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<List<WorkPlaceSDO>> result = mng.UpdateWorkPlaceList(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("UpdateWorkInfo")]
        public ApiResult UpdateWorkInfo(ApiParam<WorkInfoSDO> param)
        {
            try
            {
                TokenManager mng = new TokenManager();
                ApiResultObject<List<WorkPlaceSDO>> result = mng.UpdateWorkInfo(param.ApiData);
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
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
                ApiResultObject<bool> result = mng.RemoveOtherSession();
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
