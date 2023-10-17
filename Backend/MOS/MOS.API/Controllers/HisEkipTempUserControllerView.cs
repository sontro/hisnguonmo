using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEkipTempUser;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEkipTempUserController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEkipTempUserViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEkipTempUserViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EKIP_TEMP_USER>> result = new ApiResultObject<List<V_HIS_EKIP_TEMP_USER>>(null);
                if (param != null)
                {
                    HisEkipTempUserManager mng = new HisEkipTempUserManager(param.CommonParam);
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
