using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestPropose;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestProposeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestProposeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisImpMestProposeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_PROPOSE>> result = new ApiResultObject<List<V_HIS_IMP_MEST_PROPOSE>>(null);
                if (param != null)
                {
                    HisImpMestProposeManager mng = new HisImpMestProposeManager(param.CommonParam);
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
