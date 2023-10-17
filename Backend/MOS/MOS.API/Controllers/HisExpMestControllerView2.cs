using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest;
using MOS.MANAGER.HisExpMest.Common.Get;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestView2FilterQuery>), "param")]
        [ActionName("GetView2")]
        public ApiResult GetView2(ApiParam<HisExpMestView2FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_2>> result = new ApiResultObject<List<V_HIS_EXP_MEST_2>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetView2(param.ApiData);
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
