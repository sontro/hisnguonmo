using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisContact;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisContactController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisContactViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisContactViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CONTACT>> result = new ApiResultObject<List<V_HIS_CONTACT>>(null);
                if (param != null)
                {
                    HisContactManager mng = new HisContactManager(param.CommonParam);
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
