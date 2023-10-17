using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestMedicine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestMedicineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestMedicineView3FilterQuery>), "param")]
        [ActionName("GetView3")]
        public ApiResult GetView3(ApiParam<HisExpMestMedicineView3FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE_3>> result = new ApiResultObject<List<V_HIS_EXP_MEST_MEDICINE_3>>(null);
                if (param != null)
                {
                    HisExpMestMedicineManager mng = new HisExpMestMedicineManager(param.CommonParam);
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
