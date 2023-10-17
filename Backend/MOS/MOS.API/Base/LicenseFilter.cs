using Inventec.Common.Logging;
using MOS.MANAGER.Config;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MOS.API.Base
{
    public class LicenseFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            //if (!LicenseChecker.ValidLicense)
            //{
            //    LogSystem.Error("License khong hop le.");
            //    var response = new HttpResponseMessage()
            //    {
            //        StatusCode = System.Net.HttpStatusCode.Forbidden,
            //        Content = new StringContent("Invalid license."),
            //        RequestMessage = filterContext.Request
            //    };
            //    filterContext.Response = response;
            //}
            base.OnActionExecuting(filterContext);
        }
    }
}