using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDeathCertBook;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDeathCertBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDeathCertBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisDeathCertBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEATH_CERT_BOOK>> result = new ApiResultObject<List<V_HIS_DEATH_CERT_BOOK>>(null);
                if (param != null)
                {
                    HisDeathCertBookManager mng = new HisDeathCertBookManager(param.CommonParam);
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
