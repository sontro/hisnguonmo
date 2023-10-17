using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicineGroup;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMedicineGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicineGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMedicineGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEDICINE_GROUP>> result = new ApiResultObject<List<HIS_MEDICINE_GROUP>>(null);
                if (param != null)
                {
                    HisMedicineGroupManager mng = new HisMedicineGroupManager(param.CommonParam);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_MEDICINE_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_MEDICINE_GROUP> result = new ApiResultObject<HIS_MEDICINE_GROUP>(null);
                if (param != null)
                {
                    HisMedicineGroupManager mng = new HisMedicineGroupManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
