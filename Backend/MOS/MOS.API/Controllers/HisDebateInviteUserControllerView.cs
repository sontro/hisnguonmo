using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDebateInviteUser;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDebateInviteUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDebateInviteUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDebateInviteUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEBATE_INVITE_USER>> result = new ApiResultObject<List<V_HIS_DEBATE_INVITE_USER>>(null);
                if (param != null)
                {
                    HisDebateInviteUserManager mng = new HisDebateInviteUserManager(param.CommonParam);
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
