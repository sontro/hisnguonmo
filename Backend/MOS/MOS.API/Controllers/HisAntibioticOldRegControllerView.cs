using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAntibioticOldReg;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAntibioticOldRegController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAntibioticOldRegViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAntibioticOldRegViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTIBIOTIC_OLD_REG>> result = new ApiResultObject<List<V_HIS_ANTIBIOTIC_OLD_REG>>(null);
                if (param != null)
                {
                    HisAntibioticOldRegManager mng = new HisAntibioticOldRegManager(param.CommonParam);
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
