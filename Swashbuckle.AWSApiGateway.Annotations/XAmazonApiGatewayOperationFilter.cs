using System;
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
            void Apply<TAttribute, TOptions, TIOptions>(TIOptions source) 
                where TAttribute : Attribute, TIOptions
                where TOptions : AbstractExtensionOptions, TIOptions
            {
                var attributes = context
                                    .GetControllerAndActionAttributes<TAttribute>()
                                    ?.ToList();

                var optionsClone = Activator.CreateInstance<TOptions>();

                optionsClone.Merge(source);

                if (attributes != null && attributes.Any())
                {
                    foreach (var attribute in attributes)
                    {
                        optionsClone.Merge<TIOptions>(attribute);
                    }
                }

                foreach (var item in optionsClone.ToDictionary())
                {
                    operation.Extensions[item.Key] = item.Value;
                }
            }

            Apply<XAmazonApiGatewayIntegrationAttribute,XAmazonApiGatewayIntegrationOptions,IXAmazonApiGatewayIntegrationOptions>(_options.IntegrationOptions);
            Apply<XAmazonApiGatewayAuthAttribute,XAmazonApiGatewayAuthOptions,IXAmazonApiGatewayAuthOptions>(_options.AuthOptions);
        }
    }
}