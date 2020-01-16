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
        }
    }
}