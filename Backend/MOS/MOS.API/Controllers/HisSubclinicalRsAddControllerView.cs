using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSubclinicalRsAdd;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSubclinicalRsAddController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSubclinicalRsAddViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSubclinicalRsAddViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SUBCLINICAL_RS_ADD>> result = new ApiResultObject<List<V_HIS_SUBCLINICAL_RS_ADD>>(null);
                if (param != null)
                {
                    HisSubclinicalRsAddManager mng = new HisSubclinicalRsAddManager(param.CommonParam);
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
