using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSevereIllnessInfo;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSevereIllnessInfoController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSevereIllnessInfoViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSevereIllnessInfoViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SEVERE_ILLNESS_INFO>> result = new ApiResultObject<List<V_HIS_SEVERE_ILLNESS_INFO>>(null);
                if (param != null)
                {
                    HisSevereIllnessInfoManager mng = new HisSevereIllnessInfoManager(param.CommonParam);
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
