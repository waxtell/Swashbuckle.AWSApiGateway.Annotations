﻿using System;
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
            void Apply<TAttribute, TOptions, TIOptions>(TOptions optionsClone, TOptions source) 
                where TAttribute : AbstractTrackingAttribute, TIOptions
                where TOptions : AbstractOptions, TIOptions

            {
                var attributes = context
                                    .GetControllerAndActionAttributes<TAttribute>()
                                    ?.ToList();

                optionsClone.Merge(source);

                if (attributes != null && attributes.Any())
                {
                    foreach (var attribute in attributes)
                    {
                        optionsClone.Merge<TOptions,TIOptions,TAttribute>(attribute);
                    }
                }

                foreach (var item in optionsClone.ToDictionary())
                {
                    operation.Extensions[item.Key] = item.Value;
                }
            }

            XAmazonApiGatewayIntegrationOptions CreateDefaultIntegrationOptions(string baseUri)
            {
                return new XAmazonApiGatewayIntegrationOptions
                {
                    HttpMethod = context.ApiDescription.HttpMethod,
                    Uri = new Uri(new Uri(baseUri), context.ApiDescription.RelativePath).ToString()
                };
            }

            Apply<XAmazonApiGatewayIntegrationAttribute,XAmazonApiGatewayIntegrationOptions,IXAmazonApiGatewayIntegrationOptions>
            (
                CreateDefaultIntegrationOptions(_options.IntegrationOptions.BaseUri),
                _options.IntegrationOptions
            );

            Apply<XAmazonApiGatewayAuthAttribute,XAmazonApiGatewayAuthOptions,IXAmazonApiGatewayAuthOptions>
            (
                new XAmazonApiGatewayAuthOptions(), 
                _options.AuthOptions
            );
        }
    }
}