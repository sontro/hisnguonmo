using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisLocationStore;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisLocationStoreController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisLocationStoreViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisLocationStoreViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_LOCATION_STORE>> result = new ApiResultObject<List<V_HIS_LOCATION_STORE>>(null);
                if (param != null)
                {
                    HisLocationStoreManager mng = new HisLocationStoreManager(param.CommonParam);
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
