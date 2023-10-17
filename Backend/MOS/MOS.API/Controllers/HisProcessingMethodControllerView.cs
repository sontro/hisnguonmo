using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisProcessingMethod;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisProcessingMethodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisProcessingMethodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisProcessingMethodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PROCESSING_METHOD>> result = new ApiResultObject<List<V_HIS_PROCESSING_METHOD>>(null);
                if (param != null)
                {
                    HisProcessingMethodManager mng = new HisProcessingMethodManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
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
