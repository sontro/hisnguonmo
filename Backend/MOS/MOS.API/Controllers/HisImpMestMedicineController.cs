using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisImpMestMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestMedicineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisImpMestMedicineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_IMP_MEST_MEDICINE>> result = new ApiResultObject<List<HIS_IMP_MEST_MEDICINE>>();
                if (param != null)
                {
                    HisImpMestMedicineManager mng = new HisImpMestMedicineManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisImpMestMedicineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisImpMestMedicineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE>>(null);
                if (param != null)
                {
                    HisImpMestMedicineManager mng = new HisImpMestMedicineManager(param.CommonParam);
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
