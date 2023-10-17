using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDataStore;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDataStoreController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDataStoreView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisDataStoreView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DATA_STORE_1>> result = new ApiResultObject<List<V_HIS_DATA_STORE_1>>(null);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
