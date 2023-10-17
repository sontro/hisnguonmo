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
        [ApiParamFilter(typeof(ApiParam<HisTreatmentView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisTreatmentView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_1>> result = new ApiResultObject<List<V_HIS_TREATMENT_1>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTreatmentView1ImportFilter>), "param")]
        [ActionName("GetByImportView1")]
        public ApiResult GetByImportView1(ApiParam<HisTreatmentView1ImportFilter> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TREATMENT_1>> result = new ApiResultObject<List<V_HIS_TREATMENT_1>>(null);
                if (param != null)
                {
                    HisTreatmentManager mng = new HisTreatmentManager(param.CommonParam);
                    result = mng.GetByImportView1(param.ApiData);
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
