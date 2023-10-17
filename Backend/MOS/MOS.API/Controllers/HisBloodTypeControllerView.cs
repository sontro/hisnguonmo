using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBloodType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBloodTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBloodTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BLOOD_TYPE>> result = new ApiResultObject<List<V_HIS_BLOOD_TYPE>>(null);
                if (param != null)
                {
                    HisBloodTypeManager mng = new HisBloodTypeManager(param.CommonParam);
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
