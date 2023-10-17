using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRejectAlert;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisRejectAlertController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRejectAlertViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisRejectAlertViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_REJECT_ALERT>> result = new ApiResultObject<List<V_HIS_REJECT_ALERT>>(null);
                if (param != null)
                {
                    HisRejectAlertManager mng = new HisRejectAlertManager(param.CommonParam);
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
