using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediReactSum;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediReactSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediReactSumViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMediReactSumViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_REACT_SUM>> result = new ApiResultObject<List<V_HIS_MEDI_REACT_SUM>>(null);
                if (param != null)
                {
                    HisMediReactSumManager mng = new HisMediReactSumManager(param.CommonParam);
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
