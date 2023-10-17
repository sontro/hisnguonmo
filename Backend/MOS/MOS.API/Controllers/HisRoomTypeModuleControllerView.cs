using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoomTypeModule;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisRoomTypeModuleController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRoomTypeModuleViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisRoomTypeModuleViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ROOM_TYPE_MODULE>> result = new ApiResultObject<List<V_HIS_ROOM_TYPE_MODULE>>(null);
                if (param != null)
                {
                    HisRoomTypeModuleManager mng = new HisRoomTypeModuleManager(param.CommonParam);
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
