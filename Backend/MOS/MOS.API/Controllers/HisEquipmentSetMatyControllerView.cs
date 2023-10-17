using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEquipmentSetMaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisEquipmentSetMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEquipmentSetMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEquipmentSetMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EQUIPMENT_SET_MATY>> result = new ApiResultObject<List<V_HIS_EQUIPMENT_SET_MATY>>(null);
                if (param != null)
                {
                    HisEquipmentSetMatyManager mng = new HisEquipmentSetMatyManager(param.CommonParam);
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
