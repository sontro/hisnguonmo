using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServDeposit;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSereServDepositController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServDepositViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSereServDepositViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_DEPOSIT>> result = new ApiResultObject<List<V_HIS_SERE_SERV_DEPOSIT>>(null);
                if (param != null)
                {
                    HisSereServDepositManager mng = new HisSereServDepositManager(param.CommonParam);
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
