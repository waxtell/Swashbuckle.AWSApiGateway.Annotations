using System;
using Microsoft.OpenApi.Models;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    public static class OpenApiServerExtensions
    {
        public static OpenApiServer WithEndpointConfiguration(this OpenApiServer server, Action<XAmazonApiGatewayEndpointConfigurationOptions> setupAction)
        {
            var options = new XAmazonApiGatewayEndpointConfigurationOptions();

            setupAction.Invoke(options);

            foreach (var item in options.ToDictionary())
            {
                server.Extensions[item.Key] = item.Value;
            }

            return server;
        }
    }
}
