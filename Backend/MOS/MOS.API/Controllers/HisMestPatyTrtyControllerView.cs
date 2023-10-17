using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPatyTrty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMestPatyTrtyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPatyTrtyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMestPatyTrtyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PATY_TRTY>> result = new ApiResultObject<List<V_HIS_MEST_PATY_TRTY>>(null);
                if (param != null)
                {
                    HisMestPatyTrtyManager mng = new HisMestPatyTrtyManager(param.CommonParam);
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
