using Inventec.Common.Logging;
using Inventec.Token.Core;
using Inventec.Core;
using MOS.API.Base;
using MOS.SDO;
using System;
using System.Web.Http;
using System.Collections.Generic;
using MOS.MANAGER.SqlExecute;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class SqlExecuteController : BaseApiController
    {
        [HttpPost]
        [ActionName("Run")]
        public ApiResult Run(ApiParam<ExecuteSqlSDO> param)
        {
            try
            {
                SqlExecuteManager mng = new SqlExecuteManager();
                ApiResultObject<bool> result = mng.Run(param.ApiData);
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
