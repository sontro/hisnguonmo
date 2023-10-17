using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceType;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceTypeFilterQuery>), "param")]
        [ActionName("Get")]         
        public ApiResult Get(ApiParam<HisServiceTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_TYPE>> result = new ApiResultObject<List<HIS_SERVICE_TYPE>>(null);
                if (param != null)
                {
                    HisServiceTypeManager mng = new HisServiceTypeManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisServiceTypeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisServiceTypeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERVICE_TYPE>> result = new ApiResultObject<List<V_HIS_SERVICE_TYPE>>(null);
                if (param != null)
                {
                    HisServiceTypeManager mng = new HisServiceTypeManager(param.CommonParam);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<ServiceTypeUpdateSDO> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_TYPE> result = new ApiResultObject<HIS_SERVICE_TYPE>(null);
                if (param != null)
                {
                    HisServiceTypeManager mng = new HisServiceTypeManager(param.CommonParam);
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
    }
}
