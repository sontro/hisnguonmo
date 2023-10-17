using Inventec.Core;
using Inventec.Common.Logging;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExamSereDire;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisExamSereDireController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisExamSereDireFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisExamSereDireFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EXAM_SERE_DIRE>> result = new ApiResultObject<List<HIS_EXAM_SERE_DIRE>>(null);
                if (param != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisExamSereDireViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisExamSereDireViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EXAM_SERE_DIRE>> result = new ApiResultObject<List<V_HIS_EXAM_SERE_DIRE>>(null);
                if (param != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EXAM_SERE_DIRE> param)
        {
            try
            {
                ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
                if (param != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EXAM_SERE_DIRE> param)
        {
            try
            {
                ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
                if (param != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_EXAM_SERE_DIRE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<HIS_EXAM_SERE_DIRE> param)
        {
            try
            {
                ApiResultObject<HIS_EXAM_SERE_DIRE> result = new ApiResultObject<HIS_EXAM_SERE_DIRE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisExamSereDireManager mng = new HisExamSereDireManager(param.CommonParam);
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
