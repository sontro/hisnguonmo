using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMedicalContract;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMedicalContractController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMedicalContractViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMedicalContractViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDICAL_CONTRACT>> result = new ApiResultObject<List<V_HIS_MEDICAL_CONTRACT>>(null);
                if (param != null)
                {
                    HisMedicalContractManager mng = new HisMedicalContractManager(param.CommonParam);
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
