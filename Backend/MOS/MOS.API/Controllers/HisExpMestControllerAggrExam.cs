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
        [ActionName("AggrExamCreate")]
        public ApiResult AggrExamCreate(ApiParam<HisExpMestAggrSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamCreate(param.ApiData);
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
        [ActionName("AggrExamApprove")]
        public ApiResult AggrExamApprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamApprove(param.ApiData);
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
        [ActionName("AggrExamExport")]
        public ApiResult AggrExamExport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamExport(param.ApiData);
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
        [ActionName("AggrExamUnapprove")]
        public ApiResult AggrExamUnapprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamUnapprove(param.ApiData);
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
        [ActionName("AggrExamUnexport")]
        public ApiResult AggrExamUnexport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamUnexport(param.ApiData);
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
        [ActionName("AggrExamRemove")]
        public ApiResult AggrExamRemove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamRemove(param.ApiData);
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
        [ActionName("AggrExamDelete")]
        public ApiResult AggrExamDelete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamDelete(param.ApiData);
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
        [ActionName("AggrExamByTreatAndStock")]
        public ApiResult AggrExamCreate(ApiParam<AggrExamByTreatAndStockSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExamByTreatAndStock(param.ApiData);
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
