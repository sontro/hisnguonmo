using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRationSum;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisRationSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRationSumFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRationSumFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_RATION_SUM>> result = new ApiResultObject<List<HIS_RATION_SUM>>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Get(param.ApiData);
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
        [ApiParamFilter(typeof(ApiParam<HisRationSumViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisRationSumViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_RATION_SUM>> result = new ApiResultObject<List<V_HIS_RATION_SUM>>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HisRationSumSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_RATION_SUM>> result = new ApiResultObject<List<HIS_RATION_SUM>>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Create(param.ApiData);
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
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_RATION_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Update(param.ApiData);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HisRationSumSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Delete(param.ApiData);
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
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null && param.ApiData != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.ChangeLock(param.ApiData);
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
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_RATION_SUM> result = null;
            if (param != null && param.ApiData != null)
            {
                HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<HisRationSumUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Approve(param.ApiData);
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
        [ActionName("Unapprove")]
        public ApiResult Unapprove(ApiParam<HisRationSumUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Unapprove(param.ApiData);
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
        [ActionName("Reject")]
        public ApiResult Reject(ApiParam<HisRationSumUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Reject(param.ApiData);
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
        [ActionName("Unreject")]
        public ApiResult Unreject(ApiParam<HisRationSumUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Unreject(param.ApiData);
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
        [ActionName("Remove")]
        public ApiResult Remove(ApiParam<HisRationSumUpdateSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisRationSumManager mng = new HisRationSumManager(param.CommonParam);
                    result = mng.Remove(param.ApiData);
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
