using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMixedMedicine;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMixedMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMixedMedicineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMixedMedicineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MIXED_MEDICINE>> result = new ApiResultObject<List<V_HIS_MIXED_MEDICINE>>(null);
                if (param != null)
                {
                    HisMixedMedicineManager mng = new HisMixedMedicineManager(param.CommonParam);
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
