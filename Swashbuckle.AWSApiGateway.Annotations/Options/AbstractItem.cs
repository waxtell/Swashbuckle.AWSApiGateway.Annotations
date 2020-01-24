using Microsoft.OpenApi.Any;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public abstract class AbstractItem
    {
        internal abstract (string, IOpenApiAny) ToDictionaryItem();
    }
}