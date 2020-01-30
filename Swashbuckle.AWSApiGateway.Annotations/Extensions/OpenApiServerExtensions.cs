using System;
using System.Linq;
using Microsoft.OpenApi.Models;

namespace Swashbuckle.AWSApiGateway.Annotations.Extensions
{
    public static class OpenApiServerExtensions
    {
        /// <summary>
        /// Convenience extension method for adding a variable to a server instance
        /// </summary>
        /// <param name="server">The server that will receive the new variable</param>
        /// <param name="key">The variable name (e.g. basePath)</param>
        /// <param name="value">The value for the variable</param>
        /// <returns></returns>
        public static OpenApiServer WithVariable(this OpenApiServer server, string key, OpenApiServerVariable value)
        {
            server.Variables.Add(key, value);

            return server;
        }

        /// <summary>
        /// PRIVATE: For a private API.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="vpcEndpointIds"></param>
        /// <returns></returns>
        public static OpenApiServer AsPrivateEndpoint(this OpenApiServer server, params string[] vpcEndpointIds)
        {
            return
                server
                    .WithEndpointConfiguration
                    (
                        config =>
                        {
                            config.VpcEndpointIds = vpcEndpointIds.Where(x => !string.IsNullOrEmpty(x));

                            config.Types = new[] {"PRIVATE"};
                        }
                    );
        }

        /// <summary>
        /// EDGE: For an edge-optimized API and its custom domain name.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="customDomainName"></param>
        /// <returns></returns>
        public static OpenApiServer AsEdgeEndpoint(this OpenApiServer server, string customDomainName = null)
        {
            return
                server
                    .WithEndpointConfiguration
                    (
                        config =>
                        {
                            config.Types = new[] { "EDGE", customDomainName }
                                .Where(x => !string.IsNullOrEmpty(x));
                        }
                    );
        }

        /// <summary>
        /// REGIONAL: For a regional API and its custom domain name.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="customDomainName"></param>
        /// <returns></returns>
        public static OpenApiServer AsRegionalEndpoint(this OpenApiServer server, string customDomainName = null)
        {
            return
                server
                    .WithEndpointConfiguration
                    (
                        config =>
                        {
                            config.Types = new[] { "REGIONAL", customDomainName }
                                .Where(x => !string.IsNullOrEmpty(x));
                        }
                    );
        }

        internal static OpenApiServer WithEndpointConfiguration(this OpenApiServer server, Action<XAmazonApiGatewayEndpointConfigurationOptions> setupAction)
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
