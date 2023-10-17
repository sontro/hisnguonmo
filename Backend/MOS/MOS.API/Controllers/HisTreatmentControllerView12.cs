using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTreatment;
using MOS.MANAGER.HisTreatment.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTreatmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentView12FilterQuery>), "param")]
        [ActionName("GetView12")]
        public ApiResult GetView12(ApiParam<HisTreatmentView12FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_12>> result = new ApiResultObject<List<V_HIS_TREATMENT_12>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetView12(param.ApiData);
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