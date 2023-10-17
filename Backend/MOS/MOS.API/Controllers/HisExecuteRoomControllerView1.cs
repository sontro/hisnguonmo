using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExecuteRoom;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExecuteRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExecuteRoomView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisExecuteRoomView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXECUTE_ROOM_1>> result = new ApiResultObject<List<V_HIS_EXECUTE_ROOM_1>>(null);
                if (param != null)
                {
                    HisExecuteRoomManager mng = new HisExecuteRoomManager(param.CommonParam);
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
