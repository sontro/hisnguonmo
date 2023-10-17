using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisWelfareType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisWelfareTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisWelfareTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisWelfareTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_WELFARE_TYPE>> result = new ApiResultObject<List<V_HIS_WELFARE_TYPE>>(null);
                if (param != null)
                {
                    HisWelfareTypeManager mng = new HisWelfareTypeManager(param.CommonParam);
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
