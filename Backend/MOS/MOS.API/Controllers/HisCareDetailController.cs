using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCareDetail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisCareDetailController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCareDetailFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCareDetailFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARE_DETAIL>> result = new ApiResultObject<List<HIS_CARE_DETAIL>>(null);
                if (param != null)
                {
                    HisCareDetailManager mng = new HisCareDetailManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisCareDetailViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisCareDetailViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_CARE_DETAIL>> result = new ApiResultObject<List<V_HIS_CARE_DETAIL>>(null);
                if (param != null)
                {
                    HisCareDetailManager mng = new HisCareDetailManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CARE_DETAIL> param)
        {
            try
            {
                ApiResultObject<HIS_CARE_DETAIL> result = new ApiResultObject<HIS_CARE_DETAIL>(null);
                if (param != null)
                {
                    HisCareDetailManager mng = new HisCareDetailManager(param.CommonParam);
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
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_CARE_DETAIL> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCareDetailManager mng = new HisCareDetailManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_CARE_DETAIL> param)
        {
            try
            {
                ApiResultObject<HIS_CARE_DETAIL> result = new ApiResultObject<HIS_CARE_DETAIL>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCareDetailManager mng = new HisCareDetailManager(param.CommonParam);
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
