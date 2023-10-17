using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestBlood;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestBloodViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisExpMestBloodViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_BLOOD>> result = new ApiResultObject<List<V_HIS_EXP_MEST_BLOOD>>(null);
                if (param != null)
                {
                    HisExpMestBloodManager mng = new HisExpMestBloodManager(param.CommonParam);
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
