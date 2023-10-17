using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccineType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisVaccineTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccineTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisVaccineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_VACCINE_TYPE>> result = new ApiResultObject<List<V_HIS_VACCINE_TYPE>>(null);
                if (param != null)
                {
                    HisVaccineTypeManager mng = new HisVaccineTypeManager(param.CommonParam);
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
