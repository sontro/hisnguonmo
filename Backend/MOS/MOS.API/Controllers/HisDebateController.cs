using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDebate;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisDebateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDebateFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDebateFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEBATE>> result = null;
                if (param != null)
                {
                    HisDebateManager mng = new HisDebateManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_DEBATE> param)
        {
            try
            {
                ApiResultObject<HIS_DEBATE> result = null;
                if (param != null)
                {
                    HisDebateManager mng = new HisDebateManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_DEBATE> param)
        {
            try
            {
                
                ApiResultObject<HIS_DEBATE> result = null;
                if (param != null)
                {
                    HisDebateManager mng = new HisDebateManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_DEBATE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDebateManager mng = new HisDebateManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_DEBATE> param)
        {
            ApiResultObject<HIS_DEBATE> result = new ApiResultObject<HIS_DEBATE>(null);
            if (param != null && param.ApiData != null)
            {
                HisDebateManager mng = new HisDebateManager(param.CommonParam);
                result = mng.ChangeLock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("UpdateTelemedicineInfo")]
        public ApiResult UpdateTelemedicineInfo(ApiParam<DebateTelemedicineSDO> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDebateManager mng = new HisDebateManager(param.CommonParam);
                    result = mng.UpdateTelemedicineInfo(param.ApiData);
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
