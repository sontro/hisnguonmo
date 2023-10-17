using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPackageDetail;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPackageDetailController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPackageDetailViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPackageDetailViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PACKAGE_DETAIL>> result = new ApiResultObject<List<V_HIS_PACKAGE_DETAIL>>(null);
                if (param != null)
                {
                    HisPackageDetailManager mng = new HisPackageDetailManager(param.CommonParam);
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
