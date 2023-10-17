using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientTypeRoom;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisPatientTypeRoomController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPatientTypeRoomViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisPatientTypeRoomViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_PATIENT_TYPE_ROOM>> result = new ApiResultObject<List<V_HIS_PATIENT_TYPE_ROOM>>(null);
                if (param != null)
                {
                    HisPatientTypeRoomManager mng = new HisPatientTypeRoomManager(param.CommonParam);
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
