using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSkinSurgeryDesc;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSkinSurgeryDescController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSkinSurgeryDescViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSkinSurgeryDescViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SKIN_SURGERY_DESC>> result = new ApiResultObject<List<V_HIS_SKIN_SURGERY_DESC>>(null);
                if (param != null)
                {
                    HisSkinSurgeryDescManager mng = new HisSkinSurgeryDescManager(param.CommonParam);
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
