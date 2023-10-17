using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSesePtttMethod;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSesePtttMethodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSesePtttMethodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSesePtttMethodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SESE_PTTT_METHOD>> result = new ApiResultObject<List<V_HIS_SESE_PTTT_METHOD>>(null);
                if (param != null)
                {
                    HisSesePtttMethodManager mng = new HisSesePtttMethodManager(param.CommonParam);
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
