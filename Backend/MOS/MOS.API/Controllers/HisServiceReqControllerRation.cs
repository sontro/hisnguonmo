using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [ActionName("RationCreate")]
        public ApiResult RationCreate(ApiParam<HisRationServiceReqSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqListResultSDO> result = new ApiResultObject<HisServiceReqListResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.RationCreate(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [ActionName("UpdateSereServRation")]
        public ApiResult UpdateSereServRation(ApiParam<HisServiceReqRationUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HisServiceReqRationUpdateResultSDO> result = new ApiResultObject<HisServiceReqRationUpdateResultSDO>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.UpdateSereServRation(param.ApiData);
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
