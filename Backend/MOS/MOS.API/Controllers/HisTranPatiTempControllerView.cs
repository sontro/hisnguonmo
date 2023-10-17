using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisTranPatiTemp;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisTranPatiTempController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisTranPatiTempViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisTranPatiTempViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_TRAN_PATI_TEMP>> result = new ApiResultObject<List<V_HIS_TRAN_PATI_TEMP>>(null);
                if (param != null)
                {
                    HisTranPatiTempManager mng = new HisTranPatiTempManager(param.CommonParam);
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
