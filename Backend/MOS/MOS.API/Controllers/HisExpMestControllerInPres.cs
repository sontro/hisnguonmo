using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisExpMestController : BaseApiController
    {
        [HttpPost]
        [ActionName("InPresApprove")]
        public ApiResult InPresApprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.InPresApprove(param.ApiData);
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
        [ActionName("InPresExport")]
        public ApiResult InPresExport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.InPresExport(param.ApiData);
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
        [ActionName("InPresUnapprove")]
        public ApiResult InPresUnapprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.InPresUnapprove(param.ApiData);
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
        [ActionName("InPresUnexport")]
        public ApiResult InPresUnexport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.InPresUnexport(param.ApiData);
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
