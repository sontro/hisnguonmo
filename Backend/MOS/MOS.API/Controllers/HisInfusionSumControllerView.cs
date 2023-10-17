using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInfusionSum;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisInfusionSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInfusionSumViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisInfusionSumViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_INFUSION_SUM>> result = new ApiResultObject<List<V_HIS_INFUSION_SUM>>(null);
                if (param != null)
                {
                    HisInfusionSumManager mng = new HisInfusionSumManager(param.CommonParam);
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
