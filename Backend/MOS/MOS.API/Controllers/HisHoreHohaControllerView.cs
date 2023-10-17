using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisHoreHoha;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisHoreHohaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisHoreHohaViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisHoreHohaViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_HORE_HOHA>> result = new ApiResultObject<List<V_HIS_HORE_HOHA>>(null);
                if (param != null)
                {
                    HisHoreHohaManager mng = new HisHoreHohaManager(param.CommonParam);
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
