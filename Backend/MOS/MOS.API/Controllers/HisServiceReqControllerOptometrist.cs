using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [ActionName("Optometrist")]
        public ApiResult Optometrist(ApiParam<HisServiceReqOptometristSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_REQ> result = new ApiResultObject<HIS_SERVICE_REQ>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.Optometrist(param.ApiData);
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
