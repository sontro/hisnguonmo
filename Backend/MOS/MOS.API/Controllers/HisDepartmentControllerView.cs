using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepartment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDepartmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDepartmentViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisDepartmentViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DEPARTMENT>> result = new ApiResultObject<List<V_HIS_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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
