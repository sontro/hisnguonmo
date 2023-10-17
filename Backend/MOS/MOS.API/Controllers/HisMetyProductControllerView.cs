using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMetyProduct;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMetyProductController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMetyProductViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMetyProductViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_METY_PRODUCT>> result = new ApiResultObject<List<V_HIS_METY_PRODUCT>>(null);
                if (param != null)
                {
                    HisMetyProductManager mng = new HisMetyProductManager(param.CommonParam);
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
