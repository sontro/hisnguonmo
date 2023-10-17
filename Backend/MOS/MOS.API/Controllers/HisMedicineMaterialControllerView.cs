using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineMaterial;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMedicineMaterialController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineMaterialViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMedicineMaterialViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_MATERIAL>> result = new ApiResultObject<List<V_HIS_MEDICINE_MATERIAL>>(null);
                if (param != null)
                {
                    HisMedicineMaterialManager mng = new HisMedicineMaterialManager(param.CommonParam);
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
