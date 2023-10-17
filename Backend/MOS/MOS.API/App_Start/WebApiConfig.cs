using Inventec.Token;
using Inventec.Token.ResourceSystem;
using System.Web.Http;
using System.Web.Mvc;

namespace MOS.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.Formatters.RemoveAt(0);
            //config.Formatters.Insert(0, new MOS.API.Base.JilFormatter());

            config.EnableCors();

            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            //tich hop hipo
            config.Routes.MapHttpRoute(
                 name: "HipoApi",
                 routeTemplate: "v1/{controller}/{action}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );

            config.Filters.Add(new ApiAuthenticationFilter());
            AreaRegistration.RegisterAllAreas();
        }
    }
}
