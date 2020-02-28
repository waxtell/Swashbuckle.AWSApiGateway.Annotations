using System.Collections.Generic;
using System.Linq;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;
using Swashbuckle.AWSApiGateway.Annotations.Options;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    internal class OpenApiOperationFactory
    {
        private static OpenApiOperation BuildCorsOptionOperation(XAmazonApiGatewayCORSOptions options)
        {
            var response = new OpenApiResponse
            {
                Description = "Success",
                Content = new Dictionary<string, OpenApiMediaType>(),
                Headers = new Dictionary<string, OpenApiHeader>()
                    .ConditionalAdd
                    (
                        () => options?.AllowOrigins != null && options.AllowOrigins.Any(),
                        HeaderNames.AccessControlAllowOrigin,
                        ()=>new OpenApiHeader { Schema = new OpenApiSchema{ Type = "string" } }
                    )
                    .ConditionalAdd
                    (
                        () => options?.AllowMethods != null && options.AllowMethods.Any(),
                        HeaderNames.AccessControlAllowMethods,
                        () =>new OpenApiHeader { Schema = new OpenApiSchema { Type = "string" } }
                    )
                    .ConditionalAdd
                    (
                        () => options?.AllowHeaders != null && options.AllowHeaders.Any(),
                        HeaderNames.AccessControlAllowHeaders,
                        () => new OpenApiHeader { Schema = new OpenApiSchema { Type = "string" } }
                    )
                    .ConditionalAdd
                    (
                        () => options?.ExposeHeaders != null && options.ExposeHeaders.Any(),
                        HeaderNames.AccessControlExposeHeaders,
                        () => new OpenApiHeader { Schema = new OpenApiSchema { Type = "string" } }
                    )
            };

            return new OpenApiOperation
            {
                Responses = new OpenApiResponses { { "200", response } }
            };
        }

        public static OpenApiOperation FromCORSOptions(XAmazonApiGatewayCORSOptions options)
        {
            var corsOptionOperation = BuildCorsOptionOperation(options);

            var integrationOptions = new XAmazonApiGatewayIntegrationOptions
            {
                Type = IntegrationType.mock,
                PassthroughBehavior = PassthroughBehavior.WHEN_NO_MATCH,
                Responses = new Dictionary<string, IntegrationResponse>
                {
                    {
                        "200",
                        new IntegrationResponse
                        {
                            StatusCode = "200",
                            ResponseParameters = new Dictionary<string, string>()
                                .ConditionalAdd
                                (
                                    () => options?.AllowMethods != null && options.AllowMethods.Any(),
                                    $"method.response.header.{HeaderNames.AccessControlAllowMethods}",
                                    () => $"'{string.Join(",", options.AllowMethods)}'"
                                )
                                .ConditionalAdd
                                (
                                    () => options?.AllowHeaders != null && options.AllowHeaders.Any(),
                                    $"method.response.header.{HeaderNames.AccessControlAllowHeaders}",
                                    () => $"'{string.Join(",", options.AllowHeaders)}'"
                                )
                                .ConditionalAdd
                                (
                                    () => options?.AllowOrigins != null && options.AllowOrigins.Any(),
                                    $"method.response.header.{HeaderNames.AccessControlAllowOrigin}",
                                    () => $"'{string.Join(",", options.AllowOrigins)}'"
                                )
                                .ConditionalAdd
                                (
                                    () => options?.ExposeHeaders != null && options.ExposeHeaders.Any(),
                                    $"method.response.header.{HeaderNames.AccessControlExposeHeaders}",
                                    () => $"'{string.Join(",", options.ExposeHeaders)}'"
                                )
                        }
                    }
                },
                RequestTemplates = new Dictionary<string, string>
                {
                    {"application/json", "{\"statusCode\": 200}"}
                }
            };

            foreach (var item in integrationOptions.ToDictionary())
            {
                corsOptionOperation.Extensions.Add(item.Key, item.Value);
            }

            return corsOptionOperation;
        }
    }
}
