using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOperationFilter : IOperationFilter
    {
        private readonly XAmazonApiGatewayOperationOptions _options;

        public XAmazonApiGatewayOperationFilter(XAmazonApiGatewayOperationOptions options)
        {
            _options = options;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var attributes = context.GetControllerAndActionAttributes<XAmazonApiGatewayOperation>();
            
            var optionsClone = new XAmazonApiGatewayOperationOptions();

            optionsClone.Merge(_options);

            if (attributes != null && attributes.Any())
            {
                foreach (var attribute in attributes)
                {
                    optionsClone.Merge<IXAmazonApiGatewayOperationOptions>(attribute);
                }
            }

            foreach (var item in optionsClone.ToDictionary())
            {
                operation.Extensions[item.Key] = item.Value;
            }
        }
    }
}