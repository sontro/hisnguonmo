using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatmentUnlimit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentUnlimitController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentUnlimitViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisTreatmentUnlimitViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_UNLIMIT>> result = new ApiResultObject<List<V_HIS_TREATMENT_UNLIMIT>>(null);
                if (param != null)
                {
                    HisTreatmentUnlimitManager mng = new HisTreatmentUnlimitManager(param.CommonParam);
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
