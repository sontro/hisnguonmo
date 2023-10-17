using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisAnticipateMety;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisAnticipateMetyController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisAnticipateMetyFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisAnticipateMetyFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_ANTICIPATE_METY>> result = new ApiResultObject<List<HIS_ANTICIPATE_METY>>(null);
                if (param != null)
                {
                    HisAnticipateMetyManager mng = new HisAnticipateMetyManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisAnticipateMetyViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisAnticipateMetyViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_ANTICIPATE_METY>> result = new ApiResultObject<List<V_HIS_ANTICIPATE_METY>>(null);
                if (param != null)
                {
                    HisAnticipateMetyManager mng = new HisAnticipateMetyManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_ANTICIPATE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE_METY> result = new ApiResultObject<HIS_ANTICIPATE_METY>(null);
                if (param != null)
                {
                    HisAnticipateMetyManager mng = new HisAnticipateMetyManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_ANTICIPATE_METY> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisAnticipateMetyManager mng = new HisAnticipateMetyManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_ANTICIPATE_METY> param)
        {
            try
            {
                ApiResultObject<HIS_ANTICIPATE_METY> result = new ApiResultObject<HIS_ANTICIPATE_METY>(null);
                if (param != null && param.ApiData != null)
                {
                    HisAnticipateMetyManager mng = new HisAnticipateMetyManager(param.CommonParam);
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
