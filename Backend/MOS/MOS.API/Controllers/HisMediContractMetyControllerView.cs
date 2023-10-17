using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMediContractMety;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisMediContractMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMediContractMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMediContractMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEDI_CONTRACT_METY>> result = new ApiResultObject<List<V_HIS_MEDI_CONTRACT_METY>>(null);
                if (param != null)
                {
                    HisMediContractMetyManager mng = new HisMediContractMetyManager(param.CommonParam);
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
