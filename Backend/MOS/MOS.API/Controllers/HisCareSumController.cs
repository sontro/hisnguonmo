using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCareSum;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    /// <summary>
    /// Tong hop phuc hoi chuc nang
    /// </summary>
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisCareSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCareSumFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCareSumFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARE_SUM>> result = null;
                if (param != null)
                {
                    HisCareSumManager mng = new HisCareSumManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CARE_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
                if (param != null)
                {
                    HisCareSumManager mng = new HisCareSumManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_CARE_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_CARE_SUM> result = new ApiResultObject<HIS_CARE_SUM>(null);
                if (param != null)
                {
                    HisCareSumManager mng = new HisCareSumManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCareSumManager mng = new HisCareSumManager(param.CommonParam);
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
    }
}
