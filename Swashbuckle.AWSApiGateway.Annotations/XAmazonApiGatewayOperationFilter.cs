using System;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
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
            void Apply<TAttribute, TOptions, TIOptions>(TIOptions source, Action<TOptions> setupAction) 
                where TAttribute : Attribute, TIOptions
                where TOptions : AbstractExtensionOptions, TIOptions
            {
                var attributes = context
                                    .GetControllerAndActionAttributes<TAttribute>()
                                    ?.ToList();

                var optionsClone = Activator.CreateInstance<TOptions>();
                optionsClone.Merge(source);
                setupAction.Invoke(optionsClone);

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

            Apply<XAmazonApiGatewayIntegrationAttribute,XAmazonApiGatewayIntegrationOptions,IXAmazonApiGatewayIntegrationOptions>
            (
                _options.IntegrationOptions,
                instance =>
                {
                    if (string.IsNullOrEmpty(instance.HttpMethod))
                    {
                        instance.HttpMethod = context.ApiDescription.HttpMethod;
                    }

                    if (!string.IsNullOrEmpty(instance.BaseUri))
                    {
                        if (string.IsNullOrEmpty(instance.Uri))
                        {
                            instance.Uri = new Uri(new Uri(instance.BaseUri), context.ApiDescription.RelativePath)
                                            .ToString();
                        }
                        else
                        {
                            var uri = new Uri(instance.Uri);

                            if (!uri.IsAbsoluteUri)
                            {
                                instance.Uri = new Uri(new Uri(instance.BaseUri), uri).ToString();
                            }
                        }
                    }
                }
            );

            Apply<XAmazonApiGatewayAuthAttribute,XAmazonApiGatewayAuthOptions,IXAmazonApiGatewayAuthOptions>
            (
                _options.AuthOptions,
                instance => { }
            );
        }
    }
}