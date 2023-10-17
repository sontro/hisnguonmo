using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisService;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisServiceController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE>> result = new ApiResultObject<List<HIS_SERVICE>>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE>> result = new ApiResultObject<List<V_HIS_SERVICE>>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE> result = new ApiResultObject<HIS_SERVICE>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE>> result = new ApiResultObject<List<HIS_SERVICE>>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisServiceSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE> result = new ApiResultObject<HIS_SERVICE>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
                    result = mng.UpdateSdo(param.ApiData);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
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
                ApiResultObject<HIS_SERVICE> result = new ApiResultObject<HIS_SERVICE>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceViewFilterQuery>), "param")]
        [ActionName("GetViewZip")]
        public ApiResultZip GetViewZip(ApiParam<HisServiceViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE>> result = new ApiResultObject<List<V_HIS_SERVICE>>(null);
                if (param != null)
                {
                    HisServiceManager mng = new HisServiceManager(param.CommonParam);
                    result = mng.GetView(param.ApiData);
                }
                return new ApiResultZip(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

    }
}
