using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDataStore;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisDataStoreController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDataStoreFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDataStoreFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DATA_STORE>> result = new ApiResultObject<List<HIS_DATA_STORE>>(null);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisDataStoreViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisDataStoreViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DATA_STORE>> result = new ApiResultObject<List<V_HIS_DATA_STORE>>(null);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisDataStoreSDO> param)
        {
            try
            {
                ApiResultObject<HisDataStoreSDO> result = new ApiResultObject<HisDataStoreSDO>(null);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HisDataStoreSDO> param)
        {
            try
            {
                ApiResultObject<HisDataStoreSDO> result = new ApiResultObject<HisDataStoreSDO>(null);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_DATA_STORE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_DATA_STORE> param)
        {
            try
            {
                ApiResultObject<HIS_DATA_STORE> result = new ApiResultObject<HIS_DATA_STORE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDataStoreManager mng = new HisDataStoreManager(param.CommonParam);
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
    }
}
