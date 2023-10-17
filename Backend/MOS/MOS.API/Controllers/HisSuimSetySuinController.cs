using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSuimSetySuin;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSuimSetySuinController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSuimSetySuinFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSuimSetySuinFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SUIM_SETY_SUIN>> result = new ApiResultObject<List<HIS_SUIM_SETY_SUIN>>(null);
                if (param != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSuimSetySuinViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSuimSetySuinViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SUIM_SETY_SUIN>> result = new ApiResultObject<List<V_HIS_SUIM_SETY_SUIN>>(null);
                if (param != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SUIM_SETY_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
                if (param != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SUIM_SETY_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
                if (param != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SUIM_SETY_SUIN> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SUIM_SETY_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SUIM_SETY_SUIN> result = new ApiResultObject<HIS_SUIM_SETY_SUIN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSuimSetySuinManager mng = new HisSuimSetySuinManager(param.CommonParam);
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
