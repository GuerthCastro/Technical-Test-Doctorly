using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Doctorly.CodeTest.REST.Swagger
{
    public class SwaggerUIFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            //swaggerDoc.Definitions.Remove("UserDTO");
            
        }
    }
}
