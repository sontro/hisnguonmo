using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestMetyDepa;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMestMetyDepaController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestMetyDepaViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMestMetyDepaViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_METY_DEPA>> result = new ApiResultObject<List<V_HIS_MEST_METY_DEPA>>(null);
                if (param != null)
                {
                    HisMestMetyDepaManager mng = new HisMestMetyDepaManager(param.CommonParam);
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
