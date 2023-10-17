using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediStockImty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediStockImtyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediStockImtyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMediStockImtyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_STOCK_IMTY>> result = new ApiResultObject<List<V_HIS_MEDI_STOCK_IMTY>>(null);
                if (param != null)
                {
                    HisMediStockImtyManager mng = new HisMediStockImtyManager(param.CommonParam);
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
