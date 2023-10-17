using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.Filter;
using MOS.MANAGER.HisMachine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisMachineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMachineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisMachineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_MACHINE>> result = new ApiResultObject<List<HIS_MACHINE>>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
        [ApiParamFilter(typeof(ApiParam<HisMachineViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<HisMachineViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_MACHINE>> result = new ApiResultObject<List<V_HIS_MACHINE>>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_MACHINE> param)
        {
            try
            {
                ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_MACHINE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_MACHINE>> result = new ApiResultObject<List<HIS_MACHINE>>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_MACHINE> param)
        {
            try
            {
                ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_MACHINE> result = new ApiResultObject<HIS_MACHINE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
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

        [HttpPost]
        [ActionName("Lock")]
        public ApiResult Lock(ApiParam<long> param)
        {
            ApiResultObject<HIS_MACHINE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisMachineManager mng = new HisMachineManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisMachineCounterFilter>), "param")]
        [ActionName("GetCounter")]
        public ApiResult GetCounter(ApiParam<HisMachineCounterFilter> param)
        {
            try
            {
                ApiResultObject<List<HisMachineCounterSDO>> result = new ApiResultObject<List<HisMachineCounterSDO>>(null);
                if (param != null)
                {
                    HisMachineManager mng = new HisMachineManager(param.CommonParam);
                    result = mng.GetCounter(param.ApiData);
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
