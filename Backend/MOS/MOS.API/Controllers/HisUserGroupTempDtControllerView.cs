using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUserGroupTempDt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisUserGroupTempDtController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisUserGroupTempDtViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisUserGroupTempDtViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_USER_GROUP_TEMP_DT>> result = new ApiResultObject<List<V_HIS_USER_GROUP_TEMP_DT>>(null);
                if (param != null)
                {
                    HisUserGroupTempDtManager mng = new HisUserGroupTempDtManager(param.CommonParam);
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
