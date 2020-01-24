using Microsoft.OpenApi.Any;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    public static class OpenApiObjectExtensions
    {
        public static void Add(this OpenApiObject obj, (string key, IOpenApiAny value) item)
        {
            obj.Add(item.key, item.value);
        }
    }
}
