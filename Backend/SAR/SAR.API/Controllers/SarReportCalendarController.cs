using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarReportCalendarController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarReportCalendar.Get.SarReportCalendarFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarReportCalendar.Get.SarReportCalendarFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_REPORT_CALENDAR>> result = new ApiResultObject<List<SAR_REPORT_CALENDAR>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportCalendarManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_REPORT_CALENDAR> resultData = managerContainer.Run<List<SAR_REPORT_CALENDAR>>();
                    result = PackResult<List<SAR_REPORT_CALENDAR>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_REPORT_CALENDAR> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_CALENDAR> result = new ApiResultObject<SAR_REPORT_CALENDAR>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportCalendarManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_CALENDAR resultData = managerContainer.Run<SAR_REPORT_CALENDAR>();
                    result = PackResult<SAR_REPORT_CALENDAR>(resultData);
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
        public ApiResult Update(ApiParam<SAR_REPORT_CALENDAR> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_CALENDAR> result = new ApiResultObject<SAR_REPORT_CALENDAR>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportCalendarManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_CALENDAR resultData = managerContainer.Run<SAR_REPORT_CALENDAR>();
                    result = PackResult<SAR_REPORT_CALENDAR>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_REPORT_CALENDAR> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_CALENDAR> result = new ApiResultObject<SAR_REPORT_CALENDAR>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportCalendarManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_CALENDAR resultData = managerContainer.Run<SAR_REPORT_CALENDAR>();
                    result = PackResult<SAR_REPORT_CALENDAR>(resultData);
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
        public ApiResult Delete(ApiParam<SAR_REPORT_CALENDAR> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportCalendarManager), "Delete", new object[] { param.CommonParam }, new object[] { param.ApiData });
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
