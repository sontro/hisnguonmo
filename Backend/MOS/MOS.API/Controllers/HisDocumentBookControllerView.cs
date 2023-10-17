using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDocumentBook;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDocumentBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDocumentBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDocumentBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DOCUMENT_BOOK>> result = new ApiResultObject<List<V_HIS_DOCUMENT_BOOK>>(null);
                if (param != null)
                {
                    HisDocumentBookManager mng = new HisDocumentBookManager(param.CommonParam);
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
