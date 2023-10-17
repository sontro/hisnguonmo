using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUserAccountBook;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisUserAccountBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisUserAccountBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisUserAccountBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_USER_ACCOUNT_BOOK>> result = new ApiResultObject<List<V_HIS_USER_ACCOUNT_BOOK>>(null);
                if (param != null)
                {
                    HisUserAccountBookManager mng = new HisUserAccountBookManager(param.CommonParam);
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
