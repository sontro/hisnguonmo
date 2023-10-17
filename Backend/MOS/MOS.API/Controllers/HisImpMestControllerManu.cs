using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisImpMest;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisImpMestController : BaseApiController
    {

        [HttpPost]
        [ActionName("ManuCreate")]
        public ApiResult ManuCreate(ApiParam<HisImpMestManuSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestManuSDO> result = new ApiResultObject<HisImpMestManuSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
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
        public ApiResult ManuUpdate(ApiParam<HisImpMestManuSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestManuSDO> result = new ApiResultObject<HisImpMestManuSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("ManuUpdateInfo")]
        public ApiResult ManuUpdateInfo(ApiParam<HIS_IMP_MEST> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.ManuUpdateInfo(param.ApiData);
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
