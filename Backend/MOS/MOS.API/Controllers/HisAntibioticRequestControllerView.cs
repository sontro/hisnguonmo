using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAntibioticRequest;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAntibioticRequestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAntibioticRequestViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAntibioticRequestViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTIBIOTIC_REQUEST>> result = new ApiResultObject<List<V_HIS_ANTIBIOTIC_REQUEST>>(null);
                if (param != null)
                {
                    HisAntibioticRequestManager mng = new HisAntibioticRequestManager(param.CommonParam);
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
