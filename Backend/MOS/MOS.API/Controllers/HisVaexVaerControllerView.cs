using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaexVaer;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisVaexVaerController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaexVaerViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisVaexVaerViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_VAEX_VAER>> result = new ApiResultObject<List<V_HIS_VAEX_VAER>>(null);
                if (param != null)
                {
                    HisVaexVaerManager mng = new HisVaexVaerManager(param.CommonParam);
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
