using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisTreatment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentView3FilterQuery>), "param")]
        [ActionName("GetView3")]
        public ApiResult GetView3(ApiParam<HisTreatmentView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_3>> result = new ApiResultObject<List<V_HIS_TREATMENT_3>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetView3(param.ApiData);
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
