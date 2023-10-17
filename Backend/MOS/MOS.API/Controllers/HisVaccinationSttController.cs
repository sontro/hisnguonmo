using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisVaccinationStt;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisVaccinationSttController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisVaccinationSttFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisVaccinationSttFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_VACCINATION_STT>> result = new ApiResultObject<List<HIS_VACCINATION_STT>>(null);
                if (param != null)
                {
                    HisVaccinationSttManager mng = new HisVaccinationSttManager(param.CommonParam);
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
