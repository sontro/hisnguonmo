using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.DynamicDTO;
using MOS.MANAGER.HisSereServTemp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSereServTempController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServTempFilterQuery>), "param")]
        [ActionName("GetDynamic")]
        public ApiResult GetDynamic(ApiParam<HisSereServTempFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HisSereServTempDTO>> result = new ApiResultObject<List<HisSereServTempDTO>>(null);
                if (param != null)
                {
                    HisSereServTempManager mng = new HisSereServTempManager(param.CommonParam);
                    result = mng.GetDynamic(param.ApiData);
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