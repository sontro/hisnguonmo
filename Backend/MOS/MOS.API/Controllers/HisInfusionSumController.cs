using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInfusionSum;
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
    public partial class HisInfusionSumController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInfusionSumFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisInfusionSumFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_INFUSION_SUM>> result = null;
                if (param != null)
                {
                    HisInfusionSumManager mng = new HisInfusionSumManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_INFUSION_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_INFUSION_SUM> result = new ApiResultObject<HIS_INFUSION_SUM>(null);
                if (param != null)
                {
                    HisInfusionSumManager mng = new HisInfusionSumManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_INFUSION_SUM> param)
        {
            try
            {
                ApiResultObject<HIS_INFUSION_SUM> result = new ApiResultObject<HIS_INFUSION_SUM>(null);
                if (param != null)
                {
                    HisInfusionSumManager mng = new HisInfusionSumManager(param.CommonParam);
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
                    HisInfusionSumManager mng = new HisInfusionSumManager(param.CommonParam);
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
