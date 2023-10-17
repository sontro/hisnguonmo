using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpUserTempDt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpUserTempDtController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpUserTempDtViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisImpUserTempDtViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_USER_TEMP_DT>> result = new ApiResultObject<List<V_HIS_IMP_USER_TEMP_DT>>(null);
                if (param != null)
                {
                    HisImpUserTempDtManager mng = new HisImpUserTempDtManager(param.CommonParam);
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
