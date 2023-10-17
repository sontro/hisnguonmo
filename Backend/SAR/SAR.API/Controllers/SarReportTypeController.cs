using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarReportTypeController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarReportType.Get.SarReportTypeFilterQuery>), "param")]
        [ActionName("Get")]
        [AllowAnonymous]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarReportType.Get.SarReportTypeFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_REPORT_TYPE>> result = new ApiResultObject<List<SAR_REPORT_TYPE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_REPORT_TYPE> resultData = managerContainer.Run<List<SAR_REPORT_TYPE>>();
                    result = PackResult<List<SAR_REPORT_TYPE>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_REPORT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE> result = new ApiResultObject<SAR_REPORT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE resultData = managerContainer.Run<SAR_REPORT_TYPE>();
                    result = PackResult<SAR_REPORT_TYPE>(resultData);
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
        public ApiResult Update(ApiParam<SAR_REPORT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE> result = new ApiResultObject<SAR_REPORT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE resultData = managerContainer.Run<SAR_REPORT_TYPE>();
                    result = PackResult<SAR_REPORT_TYPE>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_REPORT_TYPE> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE> result = new ApiResultObject<SAR_REPORT_TYPE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE resultData = managerContainer.Run<SAR_REPORT_TYPE>();
                    result = PackResult<SAR_REPORT_TYPE>(resultData);
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
        public ApiResult Delete(ApiParam<SAR_REPORT_TYPE> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
