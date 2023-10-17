using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisDhst;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisDhstController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDhstFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDhstFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DHST>> result = new ApiResultObject<List<HIS_DHST>>(null);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisDhstViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisDhstViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_DHST>> result = new ApiResultObject<List<V_HIS_DHST>>(null);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_DHST> param)
        {
            try
            {
                ApiResultObject<HIS_DHST> result = new ApiResultObject<HIS_DHST>(null);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_DHST> param)
        {
            try
            {
                ApiResultObject<HIS_DHST> result = new ApiResultObject<HIS_DHST>(null);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_DHST> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
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

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDhstForEmrFilter>), "param")]
        [ActionName("GetForEmr")]
        public ApiResult GetForEmr(ApiParam<HisDhstForEmrFilter> param)
        {
            try
            {
                ApiResultObject<List<HisDhstTDO>> result = new ApiResultObject<List<HisDhstTDO>>(null);
                if (param != null)
                {
                    HisDhstManager mng = new HisDhstManager(param.CommonParam);
                    result = mng.GetForEmr(param.ApiData);
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
