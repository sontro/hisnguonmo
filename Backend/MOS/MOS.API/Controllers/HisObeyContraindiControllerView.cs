using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisObeyContraindi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisObeyContraindiController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisObeyContraindiViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisObeyContraindiViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_OBEY_CONTRAINDI>> result = new ApiResultObject<List<V_HIS_OBEY_CONTRAINDI>>(null);
                if (param != null)
                {
                    HisObeyContraindiManager mng = new HisObeyContraindiManager(param.CommonParam);
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
