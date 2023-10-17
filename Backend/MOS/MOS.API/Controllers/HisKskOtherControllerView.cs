using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisKskOther;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisKskOtherController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisKskOtherViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisKskOtherViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_KSK_OTHER>> result = new ApiResultObject<List<V_HIS_KSK_OTHER>>(null);
                if (param != null)
                {
                    HisKskOtherManager mng = new HisKskOtherManager(param.CommonParam);
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
