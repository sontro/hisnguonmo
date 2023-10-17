using Inventec.Core;
using SAR.API.Base;
using SAR.EFMODEL.DataModels;
using SAR.MANAGER.Manager;
using System;
using System.Web.Mvc;

namespace SAR.API.Controllers
{
    public class SarReportTemplateUploadController : Controller
    {
        public ActionResult Run(SAR.SDO.SarReportTemplateSDO data, CommonParam param)
        {
            try
            {
                ApiResultObject<SAR_REPORT_TEMPLATE> result = new ApiResultObject<SAR_REPORT_TEMPLATE>(null, false);
                if (param != null)
                {
                    if (Request.Files.Count > 0)
                    {
                        if (data != null)
                        {
                            data.FileUpload = Request.Files;
                        }
                    }

                    if (param == null) param = new CommonParam();
                    ManagerContainer managerContainer = new ManagerContainer(typeof(SarReportTemplateManager), "Upload", new object[] { param }, new object[] { data });
                    SAR_REPORT_TEMPLATE resultData = managerContainer.Run<SAR_REPORT_TEMPLATE>();
                    result.SetValue(resultData, true, param);
                }
                return Json(new
            {
                data = result.Data,
                success = result.Success,
                message = param.GetMessage(),
            });
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
