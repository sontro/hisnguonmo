using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCare;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisCareController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCareFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCareFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARE>> result = new ApiResultObject<List<HIS_CARE>>(null);
                if (param != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CARE> param)
        {
            try
            {
                ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
                if (param != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_CARE> param)
        {
            try
            {
                ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
                if (param != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
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
        [ActionName("UpdateWithDhst")]
        public ApiResult UpdateWithDhst(ApiParam<HIS_CARE> param)
        {
            try
            {
                ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
                if (param != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
                    result = mng.UpdateWithDhst(param.ApiData);
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
        public ApiResult Delete(ApiParam<HIS_CARE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_CARE> param)
        {
            try
            {
                ApiResultObject<HIS_CARE> result = new ApiResultObject<HIS_CARE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCareManager mng = new HisCareManager(param.CommonParam);
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
