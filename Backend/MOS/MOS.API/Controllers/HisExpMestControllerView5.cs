using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.DynamicDTO;
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
        [ApiParamFilter(typeof(ApiParam<HisExpMestView5FilterQuery>), "param")]
        [ActionName("GetView5")]
        public ApiResult GetView5(ApiParam<HisExpMestView5FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXP_MEST_5>> result = new ApiResultObject<List<V_HIS_EXP_MEST_5>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetView5(param.ApiData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExpMestView5FilterQuery>), "param")]
        [ActionName("GetView5Dynamic")]
        public ApiResult GetView5Dynamic(ApiParam<HisExpMestView5FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HisExpMestView5DTO>> result = new ApiResultObject<List<HisExpMestView5DTO>>(null);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.GetView5Dynamic(param.ApiData);
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
