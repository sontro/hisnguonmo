using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHivTreatment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisHivTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHivTreatmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisHivTreatmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HIV_TREATMENT>> result = new ApiResultObject<List<V_HIS_HIV_TREATMENT>>(null);
                if (param != null)
                {
                    HisHivTreatmentManager mng = new HisHivTreatmentManager(param.CommonParam);
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
