using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineTypeTut;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMedicineTypeTutController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineTypeTutViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMedicineTypeTutViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICINE_TYPE_TUT>> result = new ApiResultObject<List<V_HIS_MEDICINE_TYPE_TUT>>(null);
                if (param != null)
                {
                    HisMedicineTypeTutManager mng = new HisMedicineTypeTutManager(param.CommonParam);
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
