using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisNoneMediService;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisNoneMediServiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisNoneMediServiceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisNoneMediServiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_NONE_MEDI_SERVICE>> result = new ApiResultObject<List<V_HIS_NONE_MEDI_SERVICE>>(null);
                if (param != null)
                {
                    HisNoneMediServiceManager mng = new HisNoneMediServiceManager(param.CommonParam);
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
