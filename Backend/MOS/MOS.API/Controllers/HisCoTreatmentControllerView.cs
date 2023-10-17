using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCoTreatment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisCoTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCoTreatmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisCoTreatmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CO_TREATMENT>> result = new ApiResultObject<List<V_HIS_CO_TREATMENT>>(null);
                if (param != null)
                {
                    HisCoTreatmentManager mng = new HisCoTreatmentManager(param.CommonParam);
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
