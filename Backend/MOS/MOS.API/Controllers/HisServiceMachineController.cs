using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceMachine;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisServiceMachineController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisServiceMachineFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisServiceMachineFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_SERVICE_MACHINE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_SERVICE_MACHINE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_SERVICE_MACHINE> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
        public ApiResult ChangeLock(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<HIS_SERVICE_MACHINE> result = new ApiResultObject<HIS_SERVICE_MACHINE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
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
            ApiResultObject<HIS_SERVICE_MACHINE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CopyByService")]
        public ApiResult CopyByService(ApiParam<HisServiceMachineCopyByServiceSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
                    result = mng.CopyByService(param.ApiData);
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
        [ActionName("CopyByMachine")]
        public ApiResult CopyByMachine(ApiParam<HisServiceMachineCopyByMachineSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_SERVICE_MACHINE>> result = new ApiResultObject<List<HIS_SERVICE_MACHINE>>(null);
                if (param != null)
                {
                    HisServiceMachineManager mng = new HisServiceMachineManager(param.CommonParam);
                    result = mng.CopyByMachine(param.ApiData);
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
