using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBid;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisBidController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBidFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBidFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BID>> result = new ApiResultObject<List<HIS_BID>>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisBidViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisBidViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BID>> result = new ApiResultObject<List<V_HIS_BID>>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBidView1FilterQuery>), "param")]
        [ActionName("GetView1")]
        public ApiResult GetView1(ApiParam<HisBidView1FilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BID_1>> result = new ApiResultObject<List<V_HIS_BID_1>>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
                    result = mng.GetView1(param.ApiData);
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
        public ApiResult Create(ApiParam<HIS_BID> param)
        {
            try
            {
                ApiResultObject<HIS_BID> result = new ApiResultObject<HIS_BID>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_BID> param)
        {
            try
            {
                ApiResultObject<HIS_BID> result = new ApiResultObject<HIS_BID>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        [ActionName("Approve")]
        public ApiResult Approve(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_BID> result = new ApiResultObject<HIS_BID>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        public ApiResult Unapprove(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_BID> result = new ApiResultObject<HIS_BID>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_BID> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_BID> param)
        {
            try
            {
                ApiResultObject<HIS_BID> result = new ApiResultObject<HIS_BID>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetViewBySupplier")]
        public ApiResult GetViewBySupplier(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_BID>> result = new ApiResultObject<List<V_HIS_BID>>(null);
                if (param != null)
                {
                    HisBidManager mng = new HisBidManager(param.CommonParam);
                    result = mng.GetViewBySupplier(param.ApiData);
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
