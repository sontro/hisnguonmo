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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentView4FilterQuery>), "param")]
        [ActionName("GetView4")]
        public ApiResult GetView4(ApiParam<HisTreatmentView4FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_4>> result = new ApiResultObject<List<V_HIS_TREATMENT_4>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetView4(param.ApiData);
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
