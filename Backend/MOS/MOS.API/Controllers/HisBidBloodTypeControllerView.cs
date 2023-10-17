using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBidBloodType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBidBloodTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBidBloodTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBidBloodTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BID_BLOOD_TYPE>> result = new ApiResultObject<List<V_HIS_BID_BLOOD_TYPE>>(null);
                if (param != null)
                {
                    HisBidBloodTypeManager mng = new HisBidBloodTypeManager(param.CommonParam);
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
