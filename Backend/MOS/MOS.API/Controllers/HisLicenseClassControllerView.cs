using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisLicenseClass;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisLicenseClassController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisLicenseClassViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisLicenseClassViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_LICENSE_CLASS>> result = new ApiResultObject<List<V_HIS_LICENSE_CLASS>>(null);
                if (param != null)
                {
                    HisLicenseClassManager mng = new HisLicenseClassManager(param.CommonParam);
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
