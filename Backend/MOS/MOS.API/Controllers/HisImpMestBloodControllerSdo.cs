using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestBlood;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewWithInStockInfo")]
        public ApiResult GetViewWithInStockInfo(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<HisImpMestBloodWithInStockInfoSDO>> result = new ApiResultObject<List<HisImpMestBloodWithInStockInfoSDO>>(null);
                if (param != null)
                {
                    HisImpMestBloodManager mng = new HisImpMestBloodManager(param.CommonParam);
                    result = mng.GetViewWithInStockInfo(param.ApiData);
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
