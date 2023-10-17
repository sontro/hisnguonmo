using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAnticipateBlty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisAnticipateBltyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAnticipateBltyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisAnticipateBltyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTICIPATE_BLTY>> result = new ApiResultObject<List<V_HIS_ANTICIPATE_BLTY>>(null);
                if (param != null)
                {
                    HisAnticipateBltyManager mng = new HisAnticipateBltyManager(param.CommonParam);
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
