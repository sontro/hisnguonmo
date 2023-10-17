using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAccidentHurt;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MOS.SDO;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAccidentHurtController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAccidentHurtFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAccidentHurtFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ACCIDENT_HURT>> result = new ApiResultObject<List<HIS_ACCIDENT_HURT>>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisAccidentHurtViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisAccidentHurtViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ACCIDENT_HURT>> result = new ApiResultObject<List<V_HIS_ACCIDENT_HURT>>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ACCIDENT_HURT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ACCIDENT_HURT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        [ActionName("CreateSdo")]
        public ApiResult CreateSdo(ApiParam<HisAccidentHurtSDO> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        [ActionName("UpdateSdo")]
        public ApiResult UpdateSdo(ApiParam<HisAccidentHurtSDO> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
                if (param != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ACCIDENT_HURT> param)
        {
            try
            {
                ApiResultObject<HIS_ACCIDENT_HURT> result = new ApiResultObject<HIS_ACCIDENT_HURT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAccidentHurtManager mng = new HisAccidentHurtManager(param.CommonParam);
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
