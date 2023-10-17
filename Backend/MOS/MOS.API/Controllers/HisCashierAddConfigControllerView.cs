using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCashierAddConfig;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisCashierAddConfigController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCashierAddConfigViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisCashierAddConfigViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CASHIER_ADD_CONFIG>> result = new ApiResultObject<List<V_HIS_CASHIER_ADD_CONFIG>>(null);
                if (param != null)
                {
                    HisCashierAddConfigManager mng = new HisCashierAddConfigManager(param.CommonParam);
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
