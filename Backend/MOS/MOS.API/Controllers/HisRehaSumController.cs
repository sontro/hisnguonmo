using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRehaSum;
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
    public class HisRehaSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisRehaSumFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisRehaSumFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_REHA_SUM>> result = null;
                if (param != null)
                {
                    HisRehaSumManager mng = new HisRehaSumManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisRehaSumSDO> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_SUM> result = new ApiResultObject<HIS_REHA_SUM>(null);
                if (param != null)
                {
                    HisRehaSumManager mng = new HisRehaSumManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_REHA_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_REHA_SUM> result = new ApiResultObject<HIS_REHA_SUM>(null);
                if (param != null)
                {
                    HisRehaSumManager mng = new HisRehaSumManager(param.CommonParam);
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
                    HisRehaSumManager mng = new HisRehaSumManager(param.CommonParam);
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
