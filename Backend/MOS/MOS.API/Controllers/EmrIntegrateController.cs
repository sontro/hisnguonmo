using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.EmrIntegrate;
using MOS.MANAGER.GrantPermission;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class EmrIntegrateController : BaseApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ActionName("DocumentStatusChange")]
        public ApiResult DocumentStatusChange(ApiParam<EmrDocumentChangeStatusSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    DocumentStatusManager mng = new DocumentStatusManager(param.CommonParam);
                    result = mng.DocumentStatusChange(param.ApiData);
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
