using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsAuthenRequest;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsAuthenRequestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<AcsAuthenRequestViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<AcsAuthenRequestViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_AUTHEN_REQUEST>> result = new ApiResultObject<List<V_ACS_AUTHEN_REQUEST>>(null);
                if (param != null)
                {
                    AcsAuthenRequestManager mng = new AcsAuthenRequestManager(param.CommonParam);
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
