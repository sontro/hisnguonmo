using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBirthCertBook;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisBirthCertBookController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBirthCertBookViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisBirthCertBookViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BIRTH_CERT_BOOK>> result = new ApiResultObject<List<V_HIS_BIRTH_CERT_BOOK>>(null);
                if (param != null)
                {
                    HisBirthCertBookManager mng = new HisBirthCertBookManager(param.CommonParam);
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
