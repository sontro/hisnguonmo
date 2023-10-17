using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAnticipateMaty;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAnticipateMatyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAnticipateMatyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAnticipateMatyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ANTICIPATE_MATY>> result = new ApiResultObject<List<HIS_ANTICIPATE_MATY>>(null);
                if (param != null)
                {
                    HisAnticipateMatyManager mng = new HisAnticipateMatyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisAnticipateMatyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisAnticipateMatyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTICIPATE_MATY>> result = new ApiResultObject<List<V_HIS_ANTICIPATE_MATY>>(null);
                if (param != null)
                {
                    HisAnticipateMatyManager mng = new HisAnticipateMatyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ANTICIPATE_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE_MATY> result = new ApiResultObject<HIS_ANTICIPATE_MATY>(null);
                if (param != null)
                {
                    HisAnticipateMatyManager mng = new HisAnticipateMatyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ANTICIPATE_MATY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAnticipateMatyManager mng = new HisAnticipateMatyManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ANTICIPATE_MATY> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE_MATY> result = new ApiResultObject<HIS_ANTICIPATE_MATY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAnticipateMatyManager mng = new HisAnticipateMatyManager(param.CommonParam);
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
