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
        [ActionName("ManuCreate")]
        public ApiResult ManuCreate(ApiParam<HisExpMestManuSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ManuCreate(param.ApiData);
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
        [ActionName("ManuUpdate")]
        public ApiResult ManuUpdate(ApiParam<HisExpMestManuSDO> param)
        {
            try
            {
                ApiResultObject<HisExpMestResultSDO> result = new ApiResultObject<HisExpMestResultSDO>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.ManuUpdate(param.ApiData);
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
