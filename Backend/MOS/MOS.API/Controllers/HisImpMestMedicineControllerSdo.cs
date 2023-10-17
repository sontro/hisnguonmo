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
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewWithInStockAmount")]
        public ApiResult GetViewWithInStockAmount(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<HisImpMestMedicineWithInStockAmountSDO>> result = new ApiResultObject<List<HisImpMestMedicineWithInStockAmountSDO>>();
                if (param != null)
                {
                    HisImpMestMedicineManager mng = new HisImpMestMedicineManager(param.CommonParam);
                    result = mng.GetViewWithInStockAmount(param.ApiData);
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
