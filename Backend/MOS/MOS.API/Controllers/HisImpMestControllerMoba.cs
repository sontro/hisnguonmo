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
        [ActionName("MobaPresCreate")]
        public ApiResult MobaPresCreate(ApiParam<HisImpMestMobaPresSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaPresCreate(param.ApiData);
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
        [ActionName("MobaBloodCreate")]
        public ApiResult MobaBloodCreate(ApiParam<HisImpMestMobaBloodSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaBloodCreate(param.ApiData);
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
        [ActionName("MobaDepaCreate")]
        public ApiResult MobaDepaCreate(ApiParam<HisImpMestMobaDepaSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaDepaCreate(param.ApiData);
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
        [ActionName("MobaSaleCreate")]
        public ApiResult MobaSaleCreate(ApiParam<HisImpMestMobaSaleSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaSaleCreate(param.ApiData);
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
        [ActionName("MobaOutPresCreate")]
        public ApiResult MobaOutPresCreate(ApiParam<HisImpMestMobaOutPresSDO> param)
        {
            try
            {
                ApiResultObject<HisImpMestResultSDO> result = new ApiResultObject<HisImpMestResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaOutPresCreate(param.ApiData);
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
        [ActionName("MobaPresCabinetCreate")]
        public ApiResult MobaPresCabinetCreate(ApiParam<HisImpMestMobaPresCabinetSDO> param)
        {
            try
            {
                ApiResultObject<List<HisImpMestResultSDO>> result = new ApiResultObject<List<HisImpMestResultSDO>>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.MobaPresCabinetCreate(param.ApiData);
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
