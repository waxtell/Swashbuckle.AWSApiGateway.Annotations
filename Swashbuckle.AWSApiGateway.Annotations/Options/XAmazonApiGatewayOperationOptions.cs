using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;

namespace Swashbuckle.AWSApiGateway.Annotations.Options
{
    public class XAmazonApiGatewayOperationOptions : AbstractExtensionOptions
    {
        private readonly XAmazonApiGatewayIntegrationOptions _integrationOptions = new XAmazonApiGatewayIntegrationOptions();
        private readonly XAmazonApiGatewayAuthOptions _authOptions = new XAmazonApiGatewayAuthOptions();

        public void WithIntegration(Action<XAmazonApiGatewayIntegrationOptions> setupAction)
        {
            setupAction.Invoke(_integrationOptions);
        }

        public void WithAuth(Action<XAmazonApiGatewayAuthOptions> setupAction)
        {
            setupAction.Invoke(_authOptions);
        }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return
                _integrationOptions
                    .ToDictionary()
                    .Union(_authOptions.ToDictionary());
        }
    }
}
