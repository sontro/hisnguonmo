using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarPrintTypeController : BaseApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarPrintType.Get.SarPrintTypeFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarPrintType.Get.SarPrintTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_PRINT_TYPE>> result = new ApiResultObject<List<SAR_PRINT_TYPE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_PRINT_TYPE> resultData = managerContainer.Run<List<SAR_PRINT_TYPE>>();
                    result = PackResult<List<SAR_PRINT_TYPE>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
        
        [HttpPost]
        [ActionName("Create")]
        public ApiResult Create(ApiParam<SAR_PRINT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE> result = new ApiResultObject<SAR_PRINT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE resultData = managerContainer.Run<SAR_PRINT_TYPE>();
                    result = PackResult<SAR_PRINT_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public ApiResult Update(ApiParam<SAR_PRINT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE> result = new ApiResultObject<SAR_PRINT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE resultData = managerContainer.Run<SAR_PRINT_TYPE>();
                    result = PackResult<SAR_PRINT_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("ChangeLock")]
        public ApiResult ChangeLock(ApiParam<SAR_PRINT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_PRINT_TYPE> result = new ApiResultObject<SAR_PRINT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_PRINT_TYPE resultData = managerContainer.Run<SAR_PRINT_TYPE>();
                    result = PackResult<SAR_PRINT_TYPE>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ApiResult Delete(ApiParam<SAR_PRINT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarPrintTypeManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    bool resultData = managerContainer.Run<bool>();
                    result = PackResult<bool>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
