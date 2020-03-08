using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    internal class XAmazonApiGatewayDocumentFilter : IDocumentFilter
    {
        private readonly XAmazonApiGatewayOptions _options;

        public XAmazonApiGatewayDocumentFilter(XAmazonApiGatewayOptions options)
        {
            _options = options;
        }

        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var item in _options.ToDictionary())
            {
                swaggerDoc.Extensions[item.Key] = item.Value;
            }

            if (_options.CorsOptions != null && _options.CorsOptions.EmitOptionsMockMethod)
            {
                foreach (var path in swaggerDoc.Paths.Where(x => !x.Value.Operations.ContainsKey(OperationType.Options)))
                {
                    path.Value.Operations[OperationType.Options] = OpenApiOperationFactory.FromCorsOptions(_options.CorsOptions, path.Value);
                }
            }
        }
    }
}