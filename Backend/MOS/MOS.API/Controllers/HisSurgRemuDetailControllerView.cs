using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSurgRemuDetail;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisSurgRemuDetailController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSurgRemuDetailViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisSurgRemuDetailViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SURG_REMU_DETAIL>> result = new ApiResultObject<List<V_HIS_SURG_REMU_DETAIL>>(null);
                if (param != null)
                {
                    HisSurgRemuDetailManager mng = new HisSurgRemuDetailManager(param.CommonParam);
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
