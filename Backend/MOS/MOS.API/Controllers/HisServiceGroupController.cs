using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceGroup;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisServiceGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_GROUP>> result = new ApiResultObject<List<HIS_SERVICE_GROUP>>(null);
                if (param != null)
                {
                    HisServiceGroupManager mng = new HisServiceGroupManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
                if (param != null)
                {
                    HisServiceGroupManager mng = new HisServiceGroupManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
                if (param != null)
                {
                    HisServiceGroupManager mng = new HisServiceGroupManager(param.CommonParam);
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
                    HIS_SERVICE_GROUP data = new HIS_SERVICE_GROUP();
                    data.ID = param.ApiData;
                    HisServiceGroupManager mng = new HisServiceGroupManager(param.CommonParam);
                    result = mng.Delete(data);
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
        public ApiResult ChangeLock(ApiParam<HIS_SERVICE_GROUP> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_GROUP> result = new ApiResultObject<HIS_SERVICE_GROUP>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceGroupManager mng = new HisServiceGroupManager(param.CommonParam);
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
