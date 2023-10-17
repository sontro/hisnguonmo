using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInvoiceDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisInvoiceDetailController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInvoiceDetailFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisInvoiceDetailFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_INVOICE_DETAIL>> result = new ApiResultObject<List<HIS_INVOICE_DETAIL>>(null);
                if (param != null)
                {
                    HisInvoiceDetailManager mng = new HisInvoiceDetailManager(param.CommonParam);
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
