using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSereServSuin;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisSereServSuinController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisSereServSuinFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisSereServSuinFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERE_SERV_SUIN>> result = new ApiResultObject<List<HIS_SERE_SERV_SUIN>>(null);
                if (param != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisSereServSuinViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisSereServSuinViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_SERE_SERV_SUIN>> result = new ApiResultObject<List<V_HIS_SERE_SERV_SUIN>>(null);
                if (param != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERE_SERV_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
                if (param != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERE_SERV_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
                if (param != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_SERE_SERV_SUIN> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERE_SERV_SUIN> param)
        {
            try
            {
                ApiResultObject<HIS_SERE_SERV_SUIN> result = new ApiResultObject<HIS_SERE_SERV_SUIN>(null);
                if (param != null && param.ApiData != null)
                {
                    HisSereServSuinManager mng = new HisSereServSuinManager(param.CommonParam);
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
