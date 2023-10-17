using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestPatySub;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public class HisMestPatySubController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMestPatySubFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMestPatySubFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATY_SUB>> result = new ApiResultObject<List<HIS_MEST_PATY_SUB>>(null);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMestPatySubViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisMestPatySubViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MEST_PATY_SUB>> result = new ApiResultObject<List<V_HIS_MEST_PATY_SUB>>(null);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MEST_PATY_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_MEST_PATY_SUB>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MEST_PATY_SUB>> result = new ApiResultObject<List<HIS_MEST_PATY_SUB>>(null);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
                    result = mng.CreateList(param.ApiData);
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
        public ApiResult Update(ApiParam<HIS_MEST_PATY_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
        public ApiResult Delete(ApiParam<HIS_MEST_PATY_SUB> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
                    result = mng.DeleteList(param.ApiData);
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
        public ApiResult Lock(ApiParam<HIS_MEST_PATY_SUB> param)
        {
            try
            {
                ApiResultObject<HIS_MEST_PATY_SUB> result = new ApiResultObject<HIS_MEST_PATY_SUB>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMestPatySubManager mng = new HisMestPatySubManager(param.CommonParam);
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
