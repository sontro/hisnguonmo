using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDebateEkipUser;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDebateEkipUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDebateEkipUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDebateEkipUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEBATE_EKIP_USER>> result = new ApiResultObject<List<V_HIS_DEBATE_EKIP_USER>>(null);
                if (param != null)
                {
                    HisDebateEkipUserManager mng = new HisDebateEkipUserManager(param.CommonParam);
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
