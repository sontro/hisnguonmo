using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAnticipate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisAnticipateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAnticipateFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAnticipateFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ANTICIPATE>> result = new ApiResultObject<List<HIS_ANTICIPATE>>(null);
                if (param != null)
                {
                    HisAnticipateManager mng = new HisAnticipateManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ANTICIPATE> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE> result = new ApiResultObject<HIS_ANTICIPATE>(null);
                if (param != null)
                {
                    HisAnticipateManager mng = new HisAnticipateManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_ANTICIPATE> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE> result = new ApiResultObject<HIS_ANTICIPATE>(null);
                if (param != null)
                {
                    HisAnticipateManager mng = new HisAnticipateManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ANTICIPATE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAnticipateManager mng = new HisAnticipateManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ANTICIPATE> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE> result = new ApiResultObject<HIS_ANTICIPATE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAnticipateManager mng = new HisAnticipateManager(param.CommonParam);
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
