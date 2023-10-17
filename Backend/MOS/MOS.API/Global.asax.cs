using AutoMapper;
using Inventec.Common.Logging;
using Inventec.Common.Scheduler;
using MOS.LibraryHein.Bhyt;
//using MOS.LibraryHein.Bhyt.HeinMediOrg;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace MOS.API
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            try
            {
                //Cau hinh log4net
                log4net.Config.XmlConfigurator.Configure();
                LogSystem.Info("Application_Start. Bat dau start Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                //Cau hinh JsonConvert
                JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.None,
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                };

                //Cau hinh api
                AreaRegistration.RegisterAllAreas();
                WebApiConfig.Register(GlobalConfiguration.Configuration);
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);

                //Check cau hinh mapper
                Mapper.AssertConfigurationIsValid();

                //Khoi tao cac cau hinh nghiep vu he thong 
                //Luu y: can chay truoc khi khoi tao job vi job co su dung cac cau hinh
                LogSystem.Info("Application_Start. Bat dau load HisConfig Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                MOS.MANAGER.Config.Loader.RefreshConfig();
                LogSystem.Info("Application_Start. Ket thuc load HisConfig Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //Khoi tao cac job scheduler
                System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    QuartzScheduler.JobProcessor.AddJob();
                });
                //
                QuartzScheduler.UserScheduler.UserSchedulerJob.Start();

                LogSystem.Info("Application_Start. Ket thuc start Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
            catch (Exception ex)
            {
                LogSystem.Error("Application_Start error: " + ex.Message + ". Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            }
        }

        protected void Application_End()
        {
            LogSystem.Error("Application_End. Time=" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
    }
}