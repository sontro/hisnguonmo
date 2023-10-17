using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInteractiveGrade;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisInteractiveGradeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInteractiveGradeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisInteractiveGradeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_INTERACTIVE_GRADE>> result = new ApiResultObject<List<V_HIS_INTERACTIVE_GRADE>>(null);
                if (param != null)
                {
                    HisInteractiveGradeManager mng = new HisInteractiveGradeManager(param.CommonParam);
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
