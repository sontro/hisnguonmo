using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCaroDepartment;
using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
    public partial class HisCaroDepartmentController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisCaroDepartmentFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisCaroDepartmentFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
        public ApiResult Create(ApiParam<HIS_CARO_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
        public ApiResult CreateList(ApiParam<List<HIS_CARO_DEPARTMENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
        public ApiResult Update(ApiParam<HIS_CARO_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
        public ApiResult Lock(ApiParam<HIS_CARO_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_CARO_DEPARTMENT> result = new ApiResultObject<HIS_CARO_DEPARTMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
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
            ApiResultObject<HIS_CARO_DEPARTMENT> result = null;
            if (param != null && param.ApiData != null)
            {
                HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
                result = mng.Lock(param.ApiData);
            }
            return new ApiResult(result, this.ActionContext);
        }

        [HttpPost]
        [ActionName("CopyByCashierRoom")]
        public ApiResult CopyByCashierRoom(ApiParam<HisCaroDepaCopyByCashierRoomSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
                    result = mng.CopyByCashierRoom(param.ApiData);
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
        [ActionName("CopyByDepartment")]
        public ApiResult CopyByDepartment(ApiParam<HisCaroDepaCopyByDepartmentSDO> param)
        {
            try
            {
                ApiResultObject<List<HIS_CARO_DEPARTMENT>> result = new ApiResultObject<List<HIS_CARO_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisCaroDepartmentManager mng = new HisCaroDepartmentManager(param.CommonParam);
                    result = mng.CopyByDepartment(param.ApiData);
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
