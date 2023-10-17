using Inventec.Common.Logging;
using Inventec.Core;
using MOS.API.Base;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDepartment;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MOS.API.Controllers
{
    public partial class HisDepartmentController : BaseApiController
    {
        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<HisDepartmentFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<HisDepartmentFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEPARTMENT>> result = new ApiResultObject<List<HIS_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<HIS_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<HIS_DEPARTMENT>> param)
        {
            try
            {
                ApiResultObject<List<HIS_DEPARTMENT>> result = new ApiResultObject<List<HIS_DEPARTMENT>>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<HIS_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<HIS_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("DeleteList")]
        public ApiResult DeleteList(ApiParam<List<long>> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false);
                if (param != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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

        [System.Web.Http.Description.ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult Lock(ApiParam<HIS_DEPARTMENT> param)
        {
            try
            {
                ApiResultObject<HIS_DEPARTMENT> result = new ApiResultObject<HIS_DEPARTMENT>(null);
                if (param != null && param.ApiData != null)
                {
                    HisDepartmentManager mng = new HisDepartmentManager(param.CommonParam);
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
