using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Web_Api_Versioning.Api
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerOptions>
    {
        public ConfigureSwaggerOptions()
        {
            
        }
        public void Configure(string? name, SwaggerOptions options)
        {
            throw new NotImplementedException();
        }

        public void Configure(SwaggerOptions options)
        {
            
        }
    }
}
