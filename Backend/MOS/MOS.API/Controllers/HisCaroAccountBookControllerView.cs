using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCaroAccountBook;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisCaroAccountBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCaroAccountBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisCaroAccountBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CARO_ACCOUNT_BOOK>> result = new ApiResultObject<List<V_HIS_CARO_ACCOUNT_BOOK>>(null);
                if (param != null)
                {
                    HisCaroAccountBookManager mng = new HisCaroAccountBookManager(param.CommonParam);
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
