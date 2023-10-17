using System.Collections.Generic;
using System.Linq;
using Swashbuckle.Swagger;
using System.Web.Http.Description;

namespace MOS.API
{
    public class SelectedControllersDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            // Identify the controllers or endpoints for which you want to regenerate the response
            var selectedApis = new List<string>
            {
                "api/HisTestServiceReq",
                "api/HisTestIndex/List",
                "api/HisDepartment/List",
                "api/HisPatientType/List",
            };

            // Loop through the paths in the Swagger document
            foreach (var pathItem in swaggerDoc.paths.ToList())
            {
                // Check if the current path corresponds to a selected Apis
                if (!selectedApis.Any(api => pathItem.Key.Contains(api)))
                {
                    // Remove paths that do not correspond to selected Apis
                    swaggerDoc.paths.Remove(pathItem.Key);
                }
            }
        }
    }
}