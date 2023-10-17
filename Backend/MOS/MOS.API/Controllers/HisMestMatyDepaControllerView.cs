using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestMatyDepa;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMestMatyDepaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestMatyDepaViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMestMatyDepaViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_MATY_DEPA>> result = new ApiResultObject<List<V_HIS_MEST_MATY_DEPA>>(null);
                if (param != null)
                {
                    HisMestMatyDepaManager mng = new HisMestMatyDepaManager(param.CommonParam);
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
