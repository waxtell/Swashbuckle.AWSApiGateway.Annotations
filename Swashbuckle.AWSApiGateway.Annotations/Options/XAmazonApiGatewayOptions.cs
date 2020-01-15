using System;
using System.Collections.Generic;
using Microsoft.OpenApi.Interfaces;
using Swashbuckle.AWSApiGateway.Annotations.Extensions;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AWSApiGateway.Annotations
{
    public class XAmazonApiGatewayOptions : AbstractExtensionOptions
    {
        private readonly XAmazonApiGatewayKeySourceOptions _apiKeySourceOptions = new XAmazonApiGatewayKeySourceOptions();
        private readonly XAmazonApiGatewayCORSOptions _corsOptions = new XAmazonApiGatewayCORSOptions();

        public void WithKeySource(Action<XAmazonApiGatewayKeySourceOptions> setupAction)
        {
            setupAction.Invoke(_apiKeySourceOptions);
        }

        public void WithCors(Action<XAmazonApiGatewayCORSOptions> setupAction)
        {
            setupAction.Invoke(_corsOptions);
        }

        internal override IDictionary<string, IOpenApiExtension> ToDictionary()
        {
            return
                _apiKeySourceOptions
                    .ToDictionary()
                    .Union(_corsOptions.ToDictionary());
        }
    }
}