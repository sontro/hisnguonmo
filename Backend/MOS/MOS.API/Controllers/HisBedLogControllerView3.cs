using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBedLog;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBedLogController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBedLogView3FilterQuery>), "param")]
        [ActionName("GetView3")]
        public ApiResult GetView3(ApiParam<HisBedLogView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BED_LOG_3>> result = new ApiResultObject<List<V_HIS_BED_LOG_3>>(null);
                if (param != null)
                {
                    HisBedLogManager mng = new HisBedLogManager(param.CommonParam);
                    result = mng.GetView3(param.ApiData);
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
