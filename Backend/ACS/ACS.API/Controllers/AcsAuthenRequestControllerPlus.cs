using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsAuthenRequest;
using System;
using System.Collections.Generic;
using System.Web.Http;
using ACS.SDO;

namespace ACS.API.Controllers
{
    public partial class AcsAuthenRequestController : BaseApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [ActionName("AuthenRequest")]
        public ApiResult AuthenRequest(ApiParam<AuthenRequestTDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
                    result = mng.AuthenRequest(param.ApiData, this.ActionContext);
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
