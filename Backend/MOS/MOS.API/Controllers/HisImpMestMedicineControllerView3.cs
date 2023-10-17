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
    public partial class HisImpMestMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestMedicineView3FilterQuery>), "param")]
        [ActionName("GetView3")]
        public ApiResult GetView1(ApiParam<HisImpMestMedicineView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE_3>> result = new ApiResultObject<List<V_HIS_IMP_MEST_MEDICINE_3>>(null);
                if (param != null)
                {
                    HisImpMestMedicineManager mng = new HisImpMestMedicineManager(param.CommonParam);
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
