using System;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
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

            XAmazonApiGatewayIntegrationOptions CreateDefaultIntegrationOptions(OperationFilterContext ctx, string baseUri)
            {
                var requestParameters =
                ctx
                    .ApiDescription
                    .ParameterDescriptions
                    .Where(x => x.Source == BindingSource.Path)
                    .ToDictionary
                    (
                        key => $"integration.request.path.{key.Name}",
                        value => $"method.request.path.{value.Name}"
                    )
                    .Union
                    (
                        context
                            .GetControllerAndActionAttributes<XAmazonApiGatewayIntegrationRequestParameterAttribute>()
                            .ToDictionary(key => key.IntegrationRequestParameter, value => value.MethodRequestParameter)
                    );

                return new XAmazonApiGatewayIntegrationOptions
                {
                    HttpMethod = ctx.ApiDescription.HttpMethod,
                    Uri = _options.IntegrationOptions.Uri == null ? Uri.IsWellFormedUriString(baseUri, UriKind.RelativeOrAbsolute)
                            ? new Uri(new Uri(baseUri), ctx.ApiDescription.RelativePath).ToString()
                            : $@"{baseUri}{(baseUri.EndsWith("/") ? string.Empty : "/")}{ctx.ApiDescription.RelativePath}"
                                : null,
                    RequestParameters = _options.IntegrationOptions.RequestParameters.Union(requestParameters)
                };
            }

            Apply<XAmazonApiGatewayIntegrationAttribute,XAmazonApiGatewayIntegrationOptions,IXAmazonApiGatewayIntegrationOptions>
            (
                CreateDefaultIntegrationOptions(context,_options.IntegrationOptions.BaseUri),
                _options.IntegrationOptions
            );

            Apply<XAmazonApiGatewayAuthAttribute,XAmazonApiGatewayAuthOptions,IXAmazonApiGatewayAuthOptions>
            (
                new XAmazonApiGatewayAuthOptions(), 
                _options.AuthOptions
            );

            Apply<XAmazonApiGatewayRequestValidatorAttribute, XAmazonApiGatewayRequestValidatorOptions, IXAmazonApiGatewayRequestValidatorOptions>
            (
                new XAmazonApiGatewayRequestValidatorOptions(),
                null
            );
       }
    }
}