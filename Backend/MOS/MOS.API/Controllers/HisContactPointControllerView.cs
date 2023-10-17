using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisContactPoint;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisContactPointController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisContactPointViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisContactPointViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CONTACT_POINT>> result = new ApiResultObject<List<V_HIS_CONTACT_POINT>>(null);
                if (param != null)
                {
                    HisContactPointManager mng = new HisContactPointManager(param.CommonParam);
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
