using Inventec.Common.Logging;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SDA.PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(SDA.API.Startup))]
namespace SDA.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
                LogSystem.Info("Begin Configuration");
                //Install-Package Microsoft.Owin -V 2.1.0
                //Install-Package Microsoft.Owin.Host.SystemWeb -Version 2.1.0
                //Install-Package Microsoft.Owin.Security -Version 2.1.0
                AppDomain.CurrentDomain.Load(typeof(HisProHub).Assembly.FullName);

                var hubConfiguration = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };
                app.MapSignalR(hubConfiguration);
                LogSystem.Info("End Configuration");
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }
    }
}