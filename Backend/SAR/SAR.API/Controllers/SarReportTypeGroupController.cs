using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace SAR.API.Controllers
{
    public partial class SarReportTypeGroupController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarReportTypeGroup.Get.SarReportTypeGroupFilterQuery>), "param")]
        [ActionName("Get")]
        public ApiResult Get(ApiParam<SAR.MANAGER.Core.SarReportTypeGroup.Get.SarReportTypeGroupFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<SAR_REPORT_TYPE_GROUP>> result = new ApiResultObject<List<SAR_REPORT_TYPE_GROUP>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeGroupManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_REPORT_TYPE_GROUP> resultData = managerContainer.Run<List<SAR_REPORT_TYPE_GROUP>>();
                    result = PackResult<List<SAR_REPORT_TYPE_GROUP>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_REPORT_TYPE_GROUP> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE_GROUP> result = new ApiResultObject<SAR_REPORT_TYPE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeGroupManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE_GROUP resultData = managerContainer.Run<SAR_REPORT_TYPE_GROUP>();
                    result = PackResult<SAR_REPORT_TYPE_GROUP>(resultData);
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
        public ApiResult Update(ApiParam<SAR_REPORT_TYPE_GROUP> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE_GROUP> result = new ApiResultObject<SAR_REPORT_TYPE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeGroupManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE_GROUP resultData = managerContainer.Run<SAR_REPORT_TYPE_GROUP>();
                    result = PackResult<SAR_REPORT_TYPE_GROUP>(resultData);
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
        public ApiResult ChangeLock(ApiParam<SAR_REPORT_TYPE_GROUP> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TYPE_GROUP> result = new ApiResultObject<SAR_REPORT_TYPE_GROUP>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeGroupManager), "ChangeLock", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TYPE_GROUP resultData = managerContainer.Run<SAR_REPORT_TYPE_GROUP>();
                    result = PackResult<SAR_REPORT_TYPE_GROUP>(resultData);
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
        public ApiResult Delete(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<bool> result = new ApiResultObject<bool>(false, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    SAR_REPORT_TYPE_GROUP data = new SAR_REPORT_TYPE_GROUP();
                    data.ID = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTypeGroupManager), "Delete", new object[] { param.CommonParam }, new object[] { data });
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
