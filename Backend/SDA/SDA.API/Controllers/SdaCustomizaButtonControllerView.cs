using Inventec.Common.Logging;
using Inventec.Core;
using SDA.API.Base;
using SDA.EFMODEL.DataModels;
using SDA.MANAGER.SdaCustomizaButton;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SDA.API.Controllers
{
    public partial class SdaCustomizaButtonController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SdaCustomizaButtonViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<SdaCustomizaButtonViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SDA_CUSTOMIZA_BUTTON>> result = new ApiResultObject<List<V_SDA_CUSTOMIZA_BUTTON>>(null);
                if (param != null)
                {
                    SdaCustomizaButtonManager mng = new SdaCustomizaButtonManager(param.CommonParam);
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
