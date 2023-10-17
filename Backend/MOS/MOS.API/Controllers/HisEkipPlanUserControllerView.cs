using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEkipPlanUser;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEkipPlanUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEkipPlanUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEkipPlanUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EKIP_PLAN_USER>> result = new ApiResultObject<List<V_HIS_EKIP_PLAN_USER>>(null);
                if (param != null)
                {
                    HisEkipPlanUserManager mng = new HisEkipPlanUserManager(param.CommonParam);
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
