using System;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public class XAmazonApiGatewayOperationOptions
    {
        internal XAmazonApiGatewayIntegrationOptions IntegrationOptions { get; } = new XAmazonApiGatewayIntegrationOptions();
        internal XAmazonApiGatewayAuthOptions AuthOptions { get; } = new XAmazonApiGatewayAuthOptions();

        /// <summary>
        /// Specifies details of the backend integration used for this method. This extension is an extended property of the OpenAPI Operation object. The result is an API Gateway integration object.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOperationOptions WithIntegration(Action<XAmazonApiGatewayIntegrationOptions> setupAction)
        {
            setupAction.Invoke(IntegrationOptions);

            return this;
        }

        /// <summary>
        /// Defines an authentication type to be applied for authentication of method invocations in API Gateway.
        /// </summary>
        /// <param name="setupAction"></param>
        public XAmazonApiGatewayOperationOptions WithAuth(Action<XAmazonApiGatewayAuthOptions> setupAction)
        {
            setupAction.Invoke(AuthOptions);

            return this;
        }
    }
}
