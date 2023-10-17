using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisInfusion;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisInfusionController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisInfusionFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisInfusionFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_INFUSION>> result = new ApiResultObject<List<HIS_INFUSION>>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisInfusionViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisInfusionViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_INFUSION>> result = new ApiResultObject<List<V_HIS_INFUSION>>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HisInfusionSDO> param)
        {
            try
            {
                ApiResultObject<HisInfusionSDO> result = new ApiResultObject<HisInfusionSDO>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
        [ActionName("Finish")]
        public ApiResult Finish(ApiParam<HIS_INFUSION> param)
        {
            try
            {
                ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
                    result = mng.Finish(param.ApiData);
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
        [ActionName("Unfinish")]
        public ApiResult Unfinish(ApiParam<HIS_INFUSION> param)
        {
            try
            {
                ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
                    result = mng.Unfinish(param.ApiData);
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
        public ApiResult Update(ApiParam<HisInfusionSDO> param)
        {
            try
            {
                ApiResultObject<HisInfusionSDO> result = new ApiResultObject<HisInfusionSDO>(null);
                if (param != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_INFUSION> param)
        {
            try
            {
                ApiResultObject<HIS_INFUSION> result = new ApiResultObject<HIS_INFUSION>(null);
                if (param != null && param.ApiData != null)
                {
                    HisInfusionManager mng = new HisInfusionManager(param.CommonParam);
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
