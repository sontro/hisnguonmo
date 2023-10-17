using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSourceMedicine;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSourceMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSourceMedicineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSourceMedicineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SOURCE_MEDICINE>> result = new ApiResultObject<List<V_HIS_SOURCE_MEDICINE>>(null);
                if (param != null)
                {
                    HisSourceMedicineManager mng = new HisSourceMedicineManager(param.CommonParam);
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
