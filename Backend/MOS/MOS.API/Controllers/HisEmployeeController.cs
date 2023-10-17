using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisEmployee;
using MOS.TDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisEmployeeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEmployeeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisEmployeeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_EMPLOYEE>> result = new ApiResultObject<List<HIS_EMPLOYEE>>(null);
                if (param != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
        [ActionName("GetUserDetailForEmr")]
        public ApiResult GetUserDetailForEmr()
        {
            try
            {
                ApiResultObject<List<GetUserDetailForEmrTDO>> result = new ApiResultObject<List<GetUserDetailForEmrTDO>>(null);

                HisEmployeeManager mng = new HisEmployeeManager();
                result = mng.GetUserDetailForEmr();
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisEmployeeViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult Get(ApiParam<HisEmployeeViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_HIS_EMPLOYEE>> result = new ApiResultObject<List<V_HIS_EMPLOYEE>>(null);
                if (param != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_EMPLOYEE> param)
        {
            try
            {
                ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
                if (param != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_EMPLOYEE>> param)
        {
            try
            {
                ApiResultObject<List<HIS_EMPLOYEE>> result = new ApiResultObject<List<HIS_EMPLOYEE>>(null);
                if (param != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_EMPLOYEE> param)
        {
            try
            {
                ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
                if (param != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
                ApiResultObject<HIS_EMPLOYEE> result = new ApiResultObject<HIS_EMPLOYEE>(null);
                if (param != null && param.ApiData != null)
                {
                    HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
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
            ApiResultObject<HIS_EMPLOYEE> result = null;
            if (param != null && param.ApiData != null)
            {
                HisEmployeeManager mng = new HisEmployeeManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }
    }
}
