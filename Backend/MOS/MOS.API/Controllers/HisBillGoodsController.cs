using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBillGoods;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBillGoodsController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBillGoodsFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBillGoodsFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BILL_GOODS>> result = new ApiResultObject<List<HIS_BILL_GOODS>>(null);
                if (param != null)
                {
                    HisBillGoodsManager mng = new HisBillGoodsManager(param.CommonParam);
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

    }
}
