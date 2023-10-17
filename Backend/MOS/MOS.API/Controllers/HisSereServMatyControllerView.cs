using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServMaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSereServMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSereServMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_MATY>> result = new ApiResultObject<List<V_HIS_SERE_SERV_MATY>>(null);
                if (param != null)
                {
                    HisSereServMatyManager mng = new HisSereServMatyManager(param.CommonParam);
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
