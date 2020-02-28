using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Swashbuckle.AWSApiGateway.Annotations
{
    internal class XAmazonApiGatewayCORSOptionsFactory
    {
        public static XAmazonApiGatewayCORSOptions FromCorsPolicy(CorsPolicy policy)
        {
            return new XAmazonApiGatewayCORSOptions
            {
                AllowOrigins = policy.Origins,
                AllowMethods = policy.Methods,
                AllowCredentials = policy.SupportsCredentials,
                AllowHeaders = policy.Headers,
                ExposeHeaders = policy.ExposedHeaders,
                MaxAge = policy.PreflightMaxAge.HasValue
                    ? (int) policy.PreflightMaxAge.Value.TotalSeconds
                    : (int?) null
            };
        }
    }
}