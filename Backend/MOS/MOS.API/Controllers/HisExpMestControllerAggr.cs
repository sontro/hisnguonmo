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
        [ActionName("AggrCreate")]
        public ApiResult AggrCreate(ApiParam<HisExpMestAggrSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        public ApiResult AggrApprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXP_MEST>> result = new ApiResultObject<List<HIS_EXP_MEST>>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ActionName("AggrExport")]
        public ApiResult AggrExport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrExport(param.ApiData);
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
        public ApiResult AggrUnapprove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
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
        [ActionName("AggrUnexport")]
        public ApiResult AggrUnexport(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrUnexport(param.ApiData);
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
        [ActionName("AggrRemove")]
        public ApiResult AggrRemove(ApiParam<HisExpMestSDO> param)
        {
            try
            {
                ApiResultObject<HIS_EXP_MEST> result = new ApiResultObject<HIS_EXP_MEST>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrRemove(param.ApiData);
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
        [ActionName("AggrDelete")]
        public ApiResult AggrDelete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null && param.ApiData != null)
                {
                    HisExpMestManager mng = new HisExpMestManager(param.CommonParam);
                    result = mng.AggrDelete(param.ApiData);
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
