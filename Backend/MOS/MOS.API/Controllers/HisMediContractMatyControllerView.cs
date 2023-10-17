using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediContractMaty;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediContractMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediContractMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMediContractMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_CONTRACT_MATY>> result = new ApiResultObject<List<V_HIS_MEDI_CONTRACT_MATY>>(null);
                if (param != null)
                {
                    HisMediContractMatyManager mng = new HisMediContractMatyManager(param.CommonParam);
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
