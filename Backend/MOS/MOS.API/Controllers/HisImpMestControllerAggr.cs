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
        [ActionName("AggrCreate")]
        public ApiResult AggrCreate(ApiParam<HisImpMestAggrSDO> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_IMP_MEST>> result = new ApiResultObject<List<V_HIS_IMP_MEST>>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.AggrCreate(param.ApiData);
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
        [ActionName("AggrApprove")]
        public ApiResult AggrApprove(ApiParam<ImpMestAggrApprovalSDO> param)
        {
            try
            {
                ApiResultObject<ImpMestAggrApprovalResultSDO> result = new ApiResultObject<ImpMestAggrApprovalResultSDO>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.AggrApprove(param.ApiData);
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
        [ActionName("AggrUnapprove")]
        public ApiResult AggrUnapprove(ApiParam<ImpMestAggrUnapprovalSDO> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.AggrUnapprove(param.ApiData);
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
        [ActionName("RemoveAggr")]
        public ApiResult RemoveAggr(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.RemoveAggr(param.ApiData);
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
        [ActionName("AggrUnimport")]
        public ApiResult AggrUnimport(ApiParam<HIS_IMP_MEST> param)
        {
            try
            {
                ApiResultObject<HIS_IMP_MEST> result = new ApiResultObject<HIS_IMP_MEST>(null);
                if (param != null)
                {
                    HisImpMestManager mng = new HisImpMestManager(param.CommonParam);
                    result = mng.AggrUnimport(param.ApiData);
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
