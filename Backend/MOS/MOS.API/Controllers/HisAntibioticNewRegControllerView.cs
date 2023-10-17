using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAntibioticNewReg;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAntibioticNewRegController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAntibioticNewRegViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAntibioticNewRegViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTIBIOTIC_NEW_REG>> result = new ApiResultObject<List<V_HIS_ANTIBIOTIC_NEW_REG>>(null);
                if (param != null)
                {
                    HisAntibioticNewRegManager mng = new HisAntibioticNewRegManager(param.CommonParam);
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
