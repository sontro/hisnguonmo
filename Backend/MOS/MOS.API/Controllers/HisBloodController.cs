using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBlood;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.Filter;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisBloodController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisBloodFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisBloodFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_BLOOD>> result = new ApiResultObject<List<HIS_BLOOD>>(null);
                if (param != null)
                {
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_BLOOD> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD> result = new ApiResultObject<HIS_BLOOD>(null);
                if (param != null)
                {
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_BLOOD> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD> result = new ApiResultObject<HIS_BLOOD>(null);
                if (param != null)
                {
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
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
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_BLOOD> param)
        {
            try
            {
                ApiResultObject<HIS_BLOOD> result = new ApiResultObject<HIS_BLOOD>(null);
                if (param != null && param.ApiData != null)
                {
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisBloodStockViewFilter>), "param")]
        [ActionName("GetInStockBloodWithTypeTree")]
        public ApiResult GetInStockBloodWithTypeTree(ApiParam<HisBloodStockViewFilter> param)
        {
            try
            {
                ApiResultObject<List<HisBloodInStockSDO>> result = null;
                if (param != null)
                {
                    HisBloodManager mng = new HisBloodManager(param.CommonParam);
                    result = mng.GetInStockBloodWithTypeTree(param.ApiData);
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
