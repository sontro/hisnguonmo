using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAdrMedicineType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAdrMedicineTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAdrMedicineTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAdrMedicineTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ADR_MEDICINE_TYPE>> result = new ApiResultObject<List<V_HIS_ADR_MEDICINE_TYPE>>(null);
                if (param != null)
                {
                    HisAdrMedicineTypeManager mng = new HisAdrMedicineTypeManager(param.CommonParam);
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
