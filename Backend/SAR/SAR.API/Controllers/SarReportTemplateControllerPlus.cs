using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using Inventec.Core;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using System.Threading.Tasks;
using SAR.SDO;

namespace SAR.API.Controllers
{
    public partial class SarReportTemplateController : BaseApiController
    {
        [HttpGet]
        [ApiParamFilter(typeof(ApiParam<SAR.MANAGER.Core.SarReportTemplate.Get.SarReportTemplateViewFilterQuery>), "param")]
        [ActionName("GetView")]
        public ApiResult GetView(ApiParam<SAR.MANAGER.Core.SarReportTemplate.Get.SarReportTemplateViewFilterQuery> param)
        {
            try
            {
                ApiResultObject<List<V_SAR_REPORT_TEMPLATE>> result = new ApiResultObject<List<V_SAR_REPORT_TEMPLATE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Get", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<V_SAR_REPORT_TEMPLATE> resultData = managerContainer.Run<List<V_SAR_REPORT_TEMPLATE>>();
                    result = PackResult<List<V_SAR_REPORT_TEMPLATE>>(resultData);
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
        public ApiResult Create(ApiParam<SAR_REPORT_TEMPLATE> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TEMPLATE> result = new ApiResultObject<SAR_REPORT_TEMPLATE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TEMPLATE resultData = managerContainer.Run<SAR_REPORT_TEMPLATE>();
                    result = PackResult<SAR_REPORT_TEMPLATE>(resultData);
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
        public ApiResult Update(ApiParam<SAR_REPORT_TEMPLATE> param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TEMPLATE> result = new ApiResultObject<SAR_REPORT_TEMPLATE>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SAR_REPORT_TEMPLATE resultData = managerContainer.Run<SAR_REPORT_TEMPLATE>();
                    result = PackResult<SAR_REPORT_TEMPLATE>(resultData);
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
        [ActionName("Download")]
        public ApiResult Download(ApiParam<long> param)
        {
            try
            {
                ApiResultObject<SarReportTemplateDownloadSDO> result = new ApiResultObject<SarReportTemplateDownloadSDO>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    //SarReportTemplateDownloadSDO dataTransfer = new SarReportTemplateDownloadSDO();
                    //dataTransfer.ReportTemplateId = param.ApiData;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Download", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    SarReportTemplateDownloadSDO resultData = managerContainer.Run<SarReportTemplateDownloadSDO>();
                    result = PackResult<SarReportTemplateDownloadSDO>(resultData);
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
        [ActionName("CreateList")]
        public ApiResult CreateList(ApiParam<List<SAR_REPORT_TEMPLATE>> param)
        {
            try
            {
                ApiResultObject<List<SAR_REPORT_TEMPLATE>> result = new ApiResultObject<List<SAR_REPORT_TEMPLATE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Create", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_REPORT_TEMPLATE> resultData = managerContainer.Run<List<SAR_REPORT_TEMPLATE>>();
                    result = PackResult<List<SAR_REPORT_TEMPLATE>>(resultData);
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
        [ActionName("UpdateList")]
        public ApiResult UpdateList(ApiParam<List<SAR_REPORT_TEMPLATE>> param)
        {
            try
            {
                ApiResultObject<List<SAR_REPORT_TEMPLATE>> result = new ApiResultObject<List<SAR_REPORT_TEMPLATE>>(null, false);
                if (param != null)
                {
                    if (param.CommonParam == null) param.CommonParam = new CommonParam();
                    this.commonParam = param.CommonParam;
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Update", new object[] { param.CommonParam }, new object[] { param.ApiData });
                    List<SAR_REPORT_TEMPLATE> resultData = managerContainer.Run<List<SAR_REPORT_TEMPLATE>>();
                    result = PackResult<List<SAR_REPORT_TEMPLATE>>(resultData);
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
