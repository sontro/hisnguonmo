using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [HttpPost]
        [ActionName("KskExecute")]
        public ApiResult KskExecute(ApiParam<HisServiceReqKskExecuteSDO> param)
        {
            try
            {
                ApiResultObject<KskExecuteResultSDO> result = new ApiResultObject<KskExecuteResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.KskExecute(param.ApiData);
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
        [ActionName("KskExecuteV2")]
        public ApiResult KskExecuteV2(ApiParam<HisServiceReqKskExecuteV2SDO> param)
        {
            try
            {
                ApiResultObject<KskExecuteResultV2SDO> result = new ApiResultObject<KskExecuteResultV2SDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.KskExecuteV2(param.ApiData);
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