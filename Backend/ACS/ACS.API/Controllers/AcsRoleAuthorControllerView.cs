using Inventec.Common.Logging;
using Inventec.Core;
using ACS.API.Base;
using ACS.EFMODEL.DataModels;
using ACS.MANAGER.AcsRoleAuthor;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace ACS.API.Controllers
{
    public partial class AcsRoleAuthorController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<AcsRoleAuthorViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<AcsRoleAuthorViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_ACS_ROLE_AUTHOR>> result = new ApiResultObject<List<V_ACS_ROLE_AUTHOR>>(null);
                if (param != null)
                {
                    AcsRoleAuthorManager mng = new AcsRoleAuthorManager(param.CommonParam);
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
