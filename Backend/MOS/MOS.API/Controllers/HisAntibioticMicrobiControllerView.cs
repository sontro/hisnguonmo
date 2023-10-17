using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAntibioticMicrobi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAntibioticMicrobiController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAntibioticMicrobiViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAntibioticMicrobiViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTIBIOTIC_MICROBI>> result = new ApiResultObject<List<V_HIS_ANTIBIOTIC_MICROBI>>(null);
                if (param != null)
                {
                    HisAntibioticMicrobiManager mng = new HisAntibioticMicrobiManager(param.CommonParam);
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
