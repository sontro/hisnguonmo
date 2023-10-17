using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoomType;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisRoomTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRoomTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRoomTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ROOM_TYPE>> result = new ApiResultObject<List<HIS_ROOM_TYPE>>(null);
                if (param != null)
                {
                    HisRoomTypeManager mng = new HisRoomTypeManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_ROOM_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_ROOM_TYPE> result = new ApiResultObject<HIS_ROOM_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRoomTypeManager mng = new HisRoomTypeManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
