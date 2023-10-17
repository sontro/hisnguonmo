using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMestBlood;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisImpMestBloodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisImpMestBloodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST_BLOOD>> result = new ApiResultObject<List<V_HIS_IMP_MEST_BLOOD>>(null);
                if (param != null)
                {
                    HisImpMestBloodManager mng = new HisImpMestBloodManager(param.CommonParam);
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
