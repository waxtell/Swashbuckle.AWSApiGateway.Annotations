using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;
using Microsoft.Net.Http.Headers;

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
                var optionsMethod = OpenApiOperationFactory.FromCORSOptions(_options.CorsOptions);

                foreach (var path in swaggerDoc.Paths.Where(x => !x.Value.Operations.ContainsKey(OperationType.Options)))
                {
                    path.Value.Operations[OperationType.Options] = optionsMethod;
                }
            }
        }
    }
}