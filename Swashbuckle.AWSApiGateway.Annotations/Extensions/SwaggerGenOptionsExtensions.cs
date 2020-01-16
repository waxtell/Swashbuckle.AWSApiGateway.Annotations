using System;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AWSApiGateway.Annotations.Options;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public static class SwaggerGenOptionsExtensions
    {
        public static void AddXAmazonApiGatewayAnnotations(this SwaggerGenOptions swaggerGenOptions, Action<XAmazonApiGatewayOptions> setupAction)
        {
            var options = new XAmazonApiGatewayOptions();

            setupAction.Invoke(options);

            swaggerGenOptions.DocumentFilter<XAmazonApiGatewayDocumentFilter>(options);
        }

        public static void AddXAmazonApiGatewayOperationAnnotations(this SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions
                .AddXAmazonApiGatewayOperationAnnotations
                (
                    options => { }
                );
        }

        public static void AddXAmazonApiGatewayOperationAnnotations(this SwaggerGenOptions swaggerGenOptions, Action<XAmazonApiGatewayOperationOptions> setupAction)
        {
            var options = new XAmazonApiGatewayOperationOptions();

            setupAction.Invoke(options);

            swaggerGenOptions.OperationFilter<XAmazonApiGatewayOperationFilter>(options);
        }
    }
}