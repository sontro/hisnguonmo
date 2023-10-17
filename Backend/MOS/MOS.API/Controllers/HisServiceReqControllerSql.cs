using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisServiceReq;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisServiceReqController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<DHisServiceReq2Filter>), "param")]
        [ActionName("GetDHisServiceReq2")]
        public ApiResult GetDHisServiceReq2(ApiParam<DHisServiceReq2Filter> param)
        {
            try
            {
                ApiResultObject<List<D_HIS_SERVICE_REQ_2>> result = new ApiResultObject<List<D_HIS_SERVICE_REQ_2>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetDHisServiceReq2(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceReqMaxNumOrderFilter>), "param")]
        [ActionName("GetMaxNumOrder")]
        [AllowAnonymous]
        public ApiResult GetMaxNumOrder(ApiParam<HisServiceReqMaxNumOrderFilter> param)
        {
            try
            {
                ApiResultObject<List<HisServiceReqMaxNumOrderSDO>> result = new ApiResultObject<List<HisServiceReqMaxNumOrderSDO>>(null);
                if (param != null)
                {
                    HisServiceReqManager mng = new HisServiceReqManager(param.CommonParam);
                    result = mng.GetMaxNumOrder(param.ApiData);
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
