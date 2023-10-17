using Swashbuckle.Swagger;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace MOS.API
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            // Check if the API action is decorated with [AllowAnonymous]
            var allowAnonymousAttribute = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();
            bool allowAnonymous = allowAnonymousAttribute != null && allowAnonymousAttribute.Count > 0;
            if (!allowAnonymous)
            {
                if (operation.parameters == null)
                    operation.parameters = new List<Parameter>();

                // Add the Authorization header parameter
                operation.parameters.Add(new Parameter
                {
                    name = "TokenCode",
                    @in = "header",
                    description = "Authorization header using the Bearer Token",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}