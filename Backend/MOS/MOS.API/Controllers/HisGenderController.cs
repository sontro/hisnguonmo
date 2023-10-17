using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisGender;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisGenderController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisGenderFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisGenderFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_GENDER>> result = new ApiResultObject<List<HIS_GENDER>>(null);
                if (param != null)
                {
                    HisGenderManager mng = new HisGenderManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
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
