using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPackingType;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisPackingTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisPackingTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisPackingTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_PACKING_TYPE>> result = new ApiResultObject<List<HIS_PACKING_TYPE>>(null);
                if (param != null)
                {
                    HisPackingTypeManager mng = new HisPackingTypeManager(param.CommonParam);
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
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_PACKING_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PACKING_TYPE> result = new ApiResultObject<HIS_PACKING_TYPE>(null);
                if (param != null)
                {
                    HisPackingTypeManager mng = new HisPackingTypeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_PACKING_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PACKING_TYPE> result = new ApiResultObject<HIS_PACKING_TYPE>(null);
                if (param != null)
                {
                    HisPackingTypeManager mng = new HisPackingTypeManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_PACKING_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisPackingTypeManager mng = new HisPackingTypeManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_PACKING_TYPE> param)
        {
            try
            {
                ApiResultObject<HIS_PACKING_TYPE> result = new ApiResultObject<HIS_PACKING_TYPE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisPackingTypeManager mng = new HisPackingTypeManager(param.CommonParam);
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
