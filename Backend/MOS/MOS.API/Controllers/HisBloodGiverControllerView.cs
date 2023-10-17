using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBloodGiver;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBloodGiverController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodGiverViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBloodGiverViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BLOOD_GIVER>> result = new ApiResultObject<List<V_HIS_BLOOD_GIVER>>(null);
                if (param != null)
                {
                    HisBloodGiverManager mng = new HisBloodGiverManager(param.CommonParam);
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
