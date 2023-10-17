using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServBill;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisSereServBillController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServBillFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServBillFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_BILL>> result = new ApiResultObject<List<HIS_SERE_SERV_BILL>>(null);
                if (param != null)
                {
                    HisSereServBillManager mng = new HisSereServBillManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
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
