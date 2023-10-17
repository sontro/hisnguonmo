using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSurgRemuneration;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSurgRemunerationController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSurgRemunerationViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSurgRemunerationViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SURG_REMUNERATION>> result = new ApiResultObject<List<V_HIS_SURG_REMUNERATION>>(null);
                if (param != null)
                {
                    HisSurgRemunerationManager mng = new HisSurgRemunerationManager(param.CommonParam);
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
