using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisBidBloodType;
using MOS.MANAGER.HisBloodType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBloodTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodTypeStockViewFilter>), "param")]
        [ActionName("GetInStockBloodType")]
        public ApiResult GetInStockBloodType(ApiParam<HisBloodTypeStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisBloodTypeInStockSDO>> result = new ApiResultObject<List<HisBloodTypeInStockSDO>>(null);
                if (param != null)
                {
                    HisBloodTypeManager mng = new HisBloodTypeManager(param.CommonParam);
                    result = mng.GetInStockBloodType(param.ApiData);
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
