using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarReportController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarReport.Get.SarReportViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<SAR.MANAGER.Core.SarReport.Get.SarReportViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SAR_REPORT>> result = new ApiResultObject<List<V_SAR_REPORT>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SAR_REPORT> resultData = managerContainer.Run<List<V_SAR_REPORT>>();
                    result = PackResult<List<V_SAR_REPORT>>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetById")]
        public ApiResult GetById(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<string>), "param")]
        [ActionName("GetByCode")]
        public ApiResult GetByCode(ApiParam<string> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
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
        [ActionName("UpdateStt")]
        public ApiResult UpdateStt(ApiParam<SAR_REPORT> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "UpdateStt", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
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
        [ActionName("UpdateNameDescription")]
        public ApiResult UpdateNameDescription(ApiParam<SAR_REPORT> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "UpdateNameDescription", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
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
        [ActionName("Send")]
        public ApiResult Send(ApiParam<SAR_REPORT> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "Send", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
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
        [ActionName("Public")]
        public ApiResult Public(ApiParam<SAR_REPORT> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT> result = new ApiResultObject<SAR_REPORT>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "Public", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT resultData = managerContainer.Run<SAR_REPORT>();
                    result = PackResult<SAR_REPORT>(resultData);
                }
                return new ApiResult(result, this.ActionContext);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }

        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<long>), "param")]
        [ActionName("GetFile")]
        [AllowAnonymous]
        public ApiResult GetFile(ApiParam<long> param)
        {
            try
            {
                FileHolder result = new FileHolder();
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportManager), "GetFile", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    result = managerContainer.Run<FileHolder>();
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
