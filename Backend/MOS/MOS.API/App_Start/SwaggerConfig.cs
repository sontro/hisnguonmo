using System.Web.Http;
using System.Linq;
using WebActivatorEx;
using MOS.API;
using Swashbuckle.Application;
using System.IO;
using System;
using System.Net.Http;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace MOS.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        // Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                        // hold additional metadata for an API. Version and title are required but you can also provide
                        // additional fields by chaining methods off SingleApiVersion.
                        //
                        c.SingleApiVersion("v1", "MOS");

                        // Each operation be assigned one or more tags which are then used by consumers for various reasons.
                        // For example, the swagger-ui groups operations according to the first tag of each operation.
                        // By default, this will be controller name but you can use the "GroupActionsBy" option to
                        // override with any value.
                        //
                        //c.GroupActionsBy(apiDesc => apiDesc.HttpMethod.ToString());

                        // If you annotate Controllers and API Types with
                        // Xml comments (http://msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                        // those comments into the generated docs and UI. You can enable this by providing the path to one or
                        // more Xml comment files.
                        //
                        c.IncludeXmlComments(GetXmlCommentsPath());

                        // If you want to post-modify "complex" Schemas once they've been generated, across the board or for a
                        // specific type, you can wire up one or more Schema filters.
                        //
                        //c.SchemaFilter<Base.SwaggerExcludeFilter>();

                        // Configure authorization (Bearer token)
                        //c.ApiKey("Bearer")
                        //    .Description("Authorization header using the Bearer scheme. Example: \"Bearer {token}\"")
                        //    .Name("Authorization")
                        //    .In("header");
                        // Set the custom operation filter
                        c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                        // Post-modify the entire Swagger document by wiring up one or more Document filters.
                        // This gives full control to modify the final SwaggerDocument. You should have a good understanding of
                        // the Swagger 2.0 spec. - https://github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md
                        // before using this option.
                        //
                        c.DocumentFilter<SelectedControllersDocumentFilter>();

                        // In contrast to WebApi, Swagger 2.0 does not include the query string component when mapping a URL
                        // to an action. As a result, Swashbuckle will raise an exception if it encounters multiple actions
                        // with the same path (sans query string) and HTTP method. You can workaround this by providing a
                        // custom strategy to pick a winner or merge the descriptions for the purposes of the Swagger docs
                        //
                        c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                        // Wrap the default SwaggerGenerator with additional behavior (e.g. caching) or provide an
                        // alternative implementation for ISwaggerProvider with the CustomProvider option.
                        //
                        //c.CustomProvider((defaultProvider) => new CachingSwaggerProvider(defaultProvider));
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.InjectStylesheet(thisAssembly, "MOS.API.SwaggerExtensions.inject.css");
                        c.InjectJavaScript(thisAssembly, "MOS.API.SwaggerExtensions.inject.js");
                        c.DisableValidator();
                    });
        }

        private static string GetXmlCommentsPath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin/MOS.API.XML");
        }

    }
}
