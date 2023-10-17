using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.MANAGER.HisProgram;

namespace MOS.API.Controllers
{
    public partial class HisProgramController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisProgramViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisProgramViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PROGRAM>> result = new ApiResultObject<List<V_HIS_PROGRAM>>(null);
                if (param != null)
                {
                    HisProgramManager mng = new HisProgramManager(param.CommonParam);
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
