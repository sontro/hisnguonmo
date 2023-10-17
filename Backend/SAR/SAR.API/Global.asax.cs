using AutoMapper;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace SAR.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                Newtonsoft.Json.JsonConvert.DefaultSettings = () => new Newtonsoft.Json.JsonSerializerSettings
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                };
                Inventec.Common.Logging.LogSystem.Info("Bat he thong.");
                new Inventec.Common.Scheduler.JobManager().ExecuteAllJobs();
                //SAR.MANAGER.Config.License.Check();

                AreaRegistration.RegisterAllAreas();

                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);

                //Check mapper
                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error("Bat he thong: co loi xay ra.", ex);
            }
        }

        protected void Application_End()
        {
            Inventec.Common.Logging.LogSystem.Info("Tat he thong.");
        }
    }
}