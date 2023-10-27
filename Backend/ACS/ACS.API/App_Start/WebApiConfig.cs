using Inventec.Token;
using Inventec.Token.ResourceSystem;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace ACS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            Inventec.Common.Logging.LogSystem.Info(Inventec.Common.Logging.LogUtil.TraceData(Inventec.Common.Logging.LogUtil.GetMemberName(() => ACS.LibraryConfig.WebConfig.IsUsingJWT), ACS.LibraryConfig.WebConfig.IsUsingJWT));
            // Web API configuration and services
            //if (ACS.LibraryConfig.WebConfig.IsUsingJWT)
            //    config.Filters.Add(new ApiAuthenticationJwtFilter());
            //else
            config.Filters.Add(new ApiAuthenticationFilter());


            AreaRegistration.RegisterAllAreas();
        }
    }
}
